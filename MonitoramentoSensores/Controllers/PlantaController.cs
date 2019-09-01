using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.Models.Area;
using MonitoramentoSensores.Models.Planta;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MonitoramentoSensores.Controllers
{
    public class PlantaController : Controller
    {
        private IPlantaBLL _plantaBLL;
        private IAreaBLL _areaBLL;
        private IEquipamentoBLL _equipamentoBLL;
        private ISensorBLL _sensorBLL;
        
        public PlantaController(IPlantaBLL plantaBLL, IAreaBLL areaBLL, IEquipamentoBLL equipamentoBLL, ISensorBLL sensorBLL)
        {
            _plantaBLL = plantaBLL;
            _areaBLL = areaBLL;
            _equipamentoBLL = equipamentoBLL;
            _sensorBLL = sensorBLL;
        }

        public async Task<ActionResult> Index()
        {
            var listaPlanta = (await _plantaBLL.ListarPlantaAsync()).Select(p => new PlantaModel(p)).ToList();

            return View(listaPlanta);
        }

        public async Task<ActionResult> RenderizarListaPlanta()
        {
            var listaPlanta = (await _plantaBLL.ListarPlantaAsync()).Select(p => new PlantaModel(p)).ToList();

            return PartialView("_ListaPlantaPartial", listaPlanta);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarPlanta(PlantaModel planta)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _plantaBLL.CadastrarPlantaAsync(planta.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao cadastrar planta"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Planta cadastrada com sucesso"
            });
        }

        public async Task<ActionResult> RetornarPlanta(int codigo)
        {
            var planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(codigo));

            return Json(new
            {
                Planta = planta
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditarPlanta(PlantaModel planta)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _plantaBLL.EditarPlantaAsync(planta.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao editar planta"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Planta editada com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> ExcluirPlanta(int codigo)
        {
            var listaArea = (await _areaBLL.ListarAreaAsync(codigo)).Select(a => new AreaModel(a)).ToList();

            if (listaArea.Count > 0)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Exclusão negada pois a planta possui áreas vinculadas"
                });

            if (!await _plantaBLL.ExcluirPlantaAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao excluir planta"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Planta excluída com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarPlanta(int codigo)
        {
            if (!await _plantaBLL.DuplicarPlantaAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao duplicar planta"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Planta duplicada com sucesso"
            });
        }
    }
}