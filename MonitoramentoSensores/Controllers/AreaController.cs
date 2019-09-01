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
        private int _itensPorPagina = 3;

        public AreaController(IPlantaBLL plantaBLL, IAreaBLL areaBLL, IEquipamentoBLL equipamentoBLL, ISensorBLL sensorBLL)
        {
            _plantaBLL = plantaBLL;
            _areaBLL = areaBLL;
            _equipamentoBLL = equipamentoBLL;
            _sensorBLL = sensorBLL;
        }

        public async Task<ActionResult> Index(int codigoPlanta)
        {
            var planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(codigoPlanta));
            planta.ListaArea = (await _areaBLL.ListarAreaAsync(codigoPlanta)).Select(p => new AreaModel(p)).ToList();

            return View(planta);
        }

        public async Task<ActionResult> RenderizarListaArea(int codigoPlanta)
        {
            var listaArea = (await _areaBLL.ListarAreaAsync(codigoPlanta)).Select(p => new AreaModel(p)).ToList();

            return PartialView("_ListaAreaPartial", listaArea);
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

        public async Task<ActionResult> Visualizar(int codigoPlanta)
        {
            var planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(codigoPlanta));
            var paginacaoAreaMod = await _areaBLL.ListarAreaPaginadaAsync(codigoPlanta, pagina: 1, itensPorPagina: _itensPorPagina);
            planta.PaginacaoArea = new PaginacaoModel<AreaModel> {
                Pagina = paginacaoAreaMod.Pagina,
                QtdePaginas = paginacaoAreaMod.QtdePaginas,
                ItensPorPagina = paginacaoAreaMod.ItensPorPagina,
                Lista = paginacaoAreaMod.Lista.Select(c => new AreaModel(c)).ToList()
            };

            foreach (var area in planta.PaginacaoArea.Lista)
            {
                area.ListaEquipamento = (area.ListaEquipamento == null ? new List<EquipamentoModel>() : area.ListaEquipamento);
                area.ListaEquipamento = (await _equipamentoBLL.ListarEquipamentoAsync(area.Codigo)).Select(v => new EquipamentoModel(v)).ToList();

                foreach (var equipamento in area.ListaEquipamento)
                {
                    equipamento.ListaSensor = (equipamento.ListaSensor == null ? new List<SensorModel>() : equipamento.ListaSensor);
                    equipamento.ListaSensor = (await _sensorBLL.ListarSensorAsync(equipamento.Codigo)).Select(s => new SensorModel(s)).ToList();
                }
            }

            return View("Visualizar", planta);
        }
    }
}