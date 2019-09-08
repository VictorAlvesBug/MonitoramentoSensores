using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.Models.Area;
using MonitoramentoSensores.Models.Planta;
using MonitoramentoSensores.Models.Sensor;
using MonitoramentoSensores.Models.Equipamento;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MonitoramentoSensores.Models;

namespace MonitoramentoSensores.Controllers
{
    public class AreaController : Controller
    {
        private IPlantaBLL _plantaBLL;
        private IAreaBLL _areaBLL;
        private IEquipamentoBLL _equipamentoBLL;
        private ISensorBLL _sensorBLL;
        private int _itensPorPagina = 5;

        public AreaController(IPlantaBLL plantaBLL, IAreaBLL areaBLL, IEquipamentoBLL equipamentoBLL, ISensorBLL sensorBLL)
        {
            _plantaBLL = plantaBLL;
            _areaBLL = areaBLL;
            _equipamentoBLL = equipamentoBLL;
            _sensorBLL = sensorBLL;
        }

        public async Task<ActionResult> Index(int codigoPlanta, int pagina = 1)
        {
            var planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(codigoPlanta));

            var paginacaoAreaMod = await _areaBLL.ListarAreaPaginadaAsync(codigoPlanta, pagina, _itensPorPagina);

            planta.PaginacaoArea = new PaginacaoModel<AreaModel>
            {
                Pagina = paginacaoAreaMod.Pagina,
                QtdePaginas = paginacaoAreaMod.QtdePaginas,
                ItensPorPagina = paginacaoAreaMod.ItensPorPagina,
                Lista = paginacaoAreaMod.Lista.Select(a => new AreaModel(a)).ToList()
            };
            
            return View(planta);
        }

        public async Task<ActionResult> RenderizarListaArea(int codigoPlanta, int pagina = 1)
        {
            var paginacaoAreaMod = await _areaBLL.ListarAreaPaginadaAsync(codigoPlanta, pagina, _itensPorPagina);

            var paginacaoArea = new PaginacaoModel<AreaModel>
            {
                Pagina = paginacaoAreaMod.Pagina,
                QtdePaginas = paginacaoAreaMod.QtdePaginas,
                ItensPorPagina = paginacaoAreaMod.ItensPorPagina,
                Lista = paginacaoAreaMod.Lista.Select(a => new AreaModel(a)).ToList()
            };

            return PartialView("_ListaAreaPartial", paginacaoArea);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarArea(AreaModel area)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _areaBLL.CadastrarAreaAsync(area.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao cadastrar área"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Área cadastrada com sucesso"
            });
        }

        public async Task<ActionResult> RetornarArea(int codigo)
        {
            var area = new AreaModel(await _areaBLL.RetornarAreaAsync(codigo));

            return Json(new
            {
                Area = area
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditarArea(AreaModel area)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _areaBLL.EditarAreaAsync(area.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao editar área"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Área editada com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> ExcluirArea(int codigo)
        {
            var listaEquipamento = (await _equipamentoBLL.ListarEquipamentoAsync(codigo)).Select(v => new EquipamentoModel(v)).ToList();

            if (listaEquipamento.Count > 0)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Exclusão negada pois a área possui equipamentos vinculadas"
                });

            if (!await _areaBLL.ExcluirAreaAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao excluir área"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Área excluída com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarArea(int codigo)
        {
            if (!await _areaBLL.DuplicarAreaAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao duplicar área"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Área duplicada com sucesso"
            });
        }
    }
}