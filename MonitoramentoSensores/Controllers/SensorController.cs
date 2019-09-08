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
    public class SensorController : Controller
    {
        private IPlantaBLL _plantaBLL;
        private IAreaBLL _areaBLL;
        private IEquipamentoBLL _equipamentoBLL;
        private ISensorBLL _sensorBLL;
        private int _itensPorPagina = 5;

        public SensorController(IPlantaBLL plantaBLL, IAreaBLL areaBLL, IEquipamentoBLL equipamentoBLL, ISensorBLL sensorBLL)
        {
            _plantaBLL = plantaBLL;
            _areaBLL = areaBLL;
            _equipamentoBLL = equipamentoBLL;
            _sensorBLL = sensorBLL;
        }

        public async Task<ActionResult> Index(int codigoEquipamento, int pagina = 1)
        {
            var equipamento = new EquipamentoModel(await _equipamentoBLL.RetornarEquipamentoAsync(codigoEquipamento));

            equipamento.Area = new AreaModel(await _areaBLL.RetornarAreaAsync(equipamento.CodigoArea));
            equipamento.Area.Planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(equipamento.Area.CodigoPlanta));

            var paginacaoSensorMod = await _sensorBLL.ListarSensorPaginadoAsync(codigoEquipamento, pagina, _itensPorPagina);

            equipamento.PaginacaoSensor = new PaginacaoModel<SensorModel>
            {
                Pagina = paginacaoSensorMod.Pagina,
                QtdePaginas = paginacaoSensorMod.QtdePaginas,
                ItensPorPagina = paginacaoSensorMod.ItensPorPagina,
                Lista = paginacaoSensorMod.Lista.Select(s => new SensorModel(s)).ToList()
            };

            return View(equipamento);
        }

        public async Task<ActionResult> RenderizarListaSensor(int codigoEquipamento, int pagina = 1)
        {
            var paginacaoSensorMod = await _sensorBLL.ListarSensorPaginadoAsync(codigoEquipamento, pagina, _itensPorPagina);

            var paginacaoSensor = new PaginacaoModel<SensorModel>
            {
                Pagina = paginacaoSensorMod.Pagina,
                QtdePaginas = paginacaoSensorMod.QtdePaginas,
                ItensPorPagina = paginacaoSensorMod.ItensPorPagina,
                Lista = paginacaoSensorMod.Lista.Select(s => new SensorModel(s)).ToList()
            };
            return PartialView("_ListaSensorPartial", paginacaoSensor);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarSensor(SensorModel sensor)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _sensorBLL.CadastrarSensorAsync(sensor.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao cadastrar sensor"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Sensor cadastrado com sucesso"
            });
        }

        public async Task<ActionResult> RetornarSensor(int codigo)
        {
            var sensor = new SensorModel(await _sensorBLL.RetornarSensorAsync(codigo));

            return Json(new
            {
                Sensor = sensor
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditarSensor(SensorModel sensor)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _sensorBLL.EditarSensorAsync(sensor.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao editar sensor"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Sensor editado com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> ExcluirSensor(int codigo)
        {
            if (!await _sensorBLL.ExcluirSensorAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao excluir sensor"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Sensor excluído com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarSensor(int codigo)
        {
            if (!await _sensorBLL.DuplicarSensorAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao duplicar sensor"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Sensor duplicado com sucesso"
            });
        }
    }
}