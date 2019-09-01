using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.DAL.Interfaces;
using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL
{
    public class PlantaBLL : IPlantaBLL
    {
        private IPlantaDAL _plantaDAL;
        private IAreaDAL _areaDAL;
        private IEquipamentoDAL _equipamentoDAL;
        private ISensorDAL _sensorDAL;

        public PlantaBLL(IPlantaDAL plantaDAL, IAreaDAL areaDAL, IEquipamentoDAL equipamentoDAL, ISensorDAL sensorDAL)
        {
            _plantaDAL = plantaDAL;
            _areaDAL = areaDAL;
            _equipamentoDAL = equipamentoDAL;
            _sensorDAL = sensorDAL;
        }

        public async Task<List<PlantaMOD>> ListarPlantaAsync()
        {
            return await _plantaDAL.ListarPlantaAsync();
        }

        public async Task<bool> CadastrarPlantaAsync(PlantaMOD planta)
        {
            return await _plantaDAL.CadastrarPlantaAsync(planta);
        }

        public async Task<PlantaMOD> RetornarPlantaAsync(int codigo)
        {
            return await _plantaDAL.RetornarPlantaAsync(codigo);
        }

        public async Task<bool> EditarPlantaAsync(PlantaMOD planta)
        {
            return await _plantaDAL.EditarPlantaAsync(planta);
        }

        public async Task<bool> ExcluirPlantaAsync(int codigo)
        {
            return await _plantaDAL.ExcluirPlantaAsync(codigo);
        }

        public async Task<bool> DuplicarPlantaAsync(int codigo)
        {
            var planta = await _plantaDAL.RetornarPlantaAsync(codigo);
            planta.Nome += " (Cópia)";

            var listaArea = await _areaDAL.ListarAreaAsync(planta.Codigo);

            var codigoNovaPlanta = await _plantaDAL.CadastrarPlantaRetornarCodigoAsync(planta);

            if (codigoNovaPlanta == 0)
                return false;

            foreach (var area in listaArea)
            {
                area.CodigoMSPlanta = codigoNovaPlanta;
                var codigoNovaArea = await _areaDAL.CadastrarAreaRetornarCodigoAsync(area);

                if (codigoNovaArea == 0)
                    return false;

                var listaEquipamento = await _equipamentoDAL.ListarEquipamentoAsync(area.Codigo);

                foreach (var equipamento in listaEquipamento)
                {
                    equipamento.CodigoMSArea = codigoNovaArea;
                    var codigoNovaEquipamento = await _equipamentoDAL.CadastrarEquipamentoRetornarCodigoAsync(equipamento);

                    if (codigoNovaEquipamento == 0)
                        return false;

                    var listaSensor = await _sensorDAL.ListarSensorAsync(equipamento.Codigo);

                    foreach (var sensor in listaSensor)
                    {
                        sensor.CodigoMSEquipamento = codigoNovaEquipamento;
                        var cadastrou = await _sensorDAL.CadastrarSensorAsync(sensor);

                        if (!cadastrou)
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
