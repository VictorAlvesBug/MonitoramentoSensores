using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.Models;
using MonitoramentoSensores.Models.Area;
using MonitoramentoSensores.Models.Equipamento;
using MonitoramentoSensores.Models.Planta;
using MonitoramentoSensores.Models.Sensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MonitoramentoSensores.Controllers
{
    public class VisualizacaoController : Controller
    {
        private IPlantaBLL _plantaBLL;
        private IAreaBLL _areaBLL;
        private IEquipamentoBLL _equipamentoBLL;
        private ISensorBLL _sensorBLL;
        private int _itensPorPagina = 1;

        public VisualizacaoController(IPlantaBLL plantaBLL, IAreaBLL areaBLL, IEquipamentoBLL equipamentoBLL, ISensorBLL sensorBLL)
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
                Lista = paginacaoAreaMod.Lista.Select(c => new AreaModel(c)).ToList()
            };

            foreach (var area in planta.PaginacaoArea.Lista)
            {
                area.ListaEquipamento = (area.ListaEquipamento == null ? new List<EquipamentoModel>() : area.ListaEquipamento);
                area.ListaEquipamento = (await _equipamentoBLL.ListarEquipamentoAsync(area.Codigo)).Select(e => new EquipamentoModel(e)).ToList();

                /*foreach (var equipamento in area.ListaEquipamento)
                {
                    equipamento.ListaSensor = (equipamento.ListaSensor == null ? new List<SensorModel>() : equipamento.ListaSensor);
                    equipamento.ListaSensor = (await _sensorBLL.ListarSensorAsync(equipamento.Codigo)).Select(s => new SensorModel(s)).ToList();
                }*/
            }

            return View("Visualizar", planta);
        }

        public async Task<ActionResult> RenderizarAreaPaginada(int codigoPlanta, int pagina = 1)
        {
            var planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(codigoPlanta));
            var paginacaoAreaMod = await _areaBLL.ListarAreaPaginadaAsync(codigoPlanta, pagina, _itensPorPagina);
            planta.PaginacaoArea = new PaginacaoModel<AreaModel>
            {
                Pagina = paginacaoAreaMod.Pagina,
                QtdePaginas = paginacaoAreaMod.QtdePaginas,
                ItensPorPagina = paginacaoAreaMod.ItensPorPagina,
                Lista = paginacaoAreaMod.Lista.Select(c => new AreaModel(c)).ToList()
            };

            foreach (var area in planta.PaginacaoArea.Lista)
            {
                area.ListaEquipamento = (area.ListaEquipamento == null ? new List<EquipamentoModel>() : area.ListaEquipamento);
                area.ListaEquipamento = (await _equipamentoBLL.ListarEquipamentoAsync(area.Codigo)).Select(e => new EquipamentoModel(e)).ToList();

                /*foreach (var equipamento in area.ListaEquipamento)
                {
                    equipamento.ListaSensor = (equipamento.ListaSensor == null ? new List<SensorModel>() : equipamento.ListaSensor);
                    equipamento.ListaSensor = (await _sensorBLL.ListarSensorAsync(equipamento.Codigo)).Select(s => new SensorModel(s)).ToList();
                }*/
            }

            return PartialView("_ListaAreaPaginadaPartial", planta);
        }

        public async Task<ActionResult> DetalhesEquipamento(int codigoEquipamento)
        {
            var equipamento = new EquipamentoModel(await _equipamentoBLL.RetornarEquipamentoAsync(codigoEquipamento));

            equipamento.ListaSensor = (equipamento.ListaSensor == null ? new List<SensorModel>() : equipamento.ListaSensor);
            equipamento.ListaSensor = (await _sensorBLL.ListarSensorAsync(equipamento.Codigo)).Select(s => new SensorModel(s)).ToList();

            return PartialView("_ModalEquipamentoDetalhesPartial", equipamento);
        }
    }
}