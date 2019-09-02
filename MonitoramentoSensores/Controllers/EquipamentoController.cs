﻿using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.Models.Area;
using MonitoramentoSensores.Models.Planta;
using MonitoramentoSensores.Models.Sensor;
using MonitoramentoSensores.Models.Equipamento;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MonitoramentoSensores.Controllers
{
    public class EquipamentoController : Controller
    {
        private IPlantaBLL _plantaBLL;
        private IAreaBLL _areaBLL;
        private IEquipamentoBLL _equipamentoBLL;
        private ISensorBLL _sensorBLL;

        public EquipamentoController(IPlantaBLL plantaBLL, IAreaBLL areaBLL, IEquipamentoBLL equipamentoBLL, ISensorBLL sensorBLL)
        {
            _plantaBLL = plantaBLL;
            _areaBLL = areaBLL;
            _equipamentoBLL = equipamentoBLL;
            _sensorBLL = sensorBLL;
        }

        public async Task<ActionResult> Index(int codigoArea)
        {
            var area = new AreaModel(await _areaBLL.RetornarAreaAsync(codigoArea));

            area.Planta = new PlantaModel(await _plantaBLL.RetornarPlantaAsync(area.CodigoPlanta));

            area.ListaEquipamento = (await _equipamentoBLL.ListarEquipamentoAsync(codigoArea)).Select(v => new EquipamentoModel(v)).ToList();

            foreach (var equipamento in area.ListaEquipamento)
            {
                equipamento.ListaSensor = (equipamento.ListaSensor == null ? new List<SensorModel>() : equipamento.ListaSensor);
                equipamento.ListaSensor = (await _sensorBLL.ListarSensorAsync(equipamento.Codigo)).Select(s => new SensorModel(s)).ToList();
            }

            return View(area);
        }

        public async Task<ActionResult> RenderizarListaEquipamento(int codigoArea)
        {
            var listaEquipamento = (await _equipamentoBLL.ListarEquipamentoAsync(codigoArea)).Select(v => new EquipamentoModel(v)).ToList();

            foreach (var equipamento in listaEquipamento)
            {
                equipamento.ListaSensor = (equipamento.ListaSensor == null ? new List<SensorModel>() : equipamento.ListaSensor);
                equipamento.ListaSensor = (await _sensorBLL.ListarSensorAsync(equipamento.Codigo)).Select(s => new SensorModel(s)).ToList();
            }

            return PartialView("_ListaEquipamentoPartial", listaEquipamento);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarEquipamento(EquipamentoModel equipamento)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _equipamentoBLL.CadastrarEquipamentoAsync(equipamento.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao cadastrar equipamento"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Equipamento cadastrado com sucesso"
            });
        }

        public async Task<ActionResult> RetornarEquipamento(int codigo)
        {
            var equipamento = new EquipamentoModel(await _equipamentoBLL.RetornarEquipamentoAsync(codigo));

            return Json(new
            {
                Equipamento = equipamento
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> EditarEquipamento(EquipamentoModel equipamento)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                });

            if (!await _equipamentoBLL.EditarEquipamentoAsync(equipamento.ToMOD()))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao editar equipamento"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Equipamento editado com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> ExcluirEquipamento(int codigo)
        {
            var listaSensor = (await _sensorBLL.ListarSensorAsync(codigo)).Select(s => new SensorModel(s)).ToList();

            if (listaSensor.Count > 0)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Exclusão negada pois o equipamento possui sensores vinculados"
                });

            if (!await _equipamentoBLL.ExcluirEquipamentoAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao excluir equipamento"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Equipamento excluído com sucesso"
            });
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarEquipamento(int codigo)
        {
            if (!await _equipamentoBLL.DuplicarEquipamentoAsync(codigo))
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao duplicar equipamento"
                });

            return Json(new
            {
                Sucesso = true,
                Mensagem = "Equipamento duplicado com sucesso"
            });
        }
    }
}