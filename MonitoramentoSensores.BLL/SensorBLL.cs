using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.DAL;
using MonitoramentoSensores.DAL.Interfaces;
using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL
{
    public class SensorBLL : ISensorBLL
    {
        private ISensorDAL _sensorDAL;

        public SensorBLL(ISensorDAL sensorDAL)
        {
            _sensorDAL = sensorDAL;
        }

        public async Task<SensorMOD> RetornarSensorAsync(int codigo)
        {
            return await _sensorDAL.RetornarSensorAsync(codigo);
        }

        public async Task<bool> CadastrarSensorAsync(SensorMOD sensor)
        {
            return await _sensorDAL.CadastrarSensorAsync(sensor);
        }

        public async Task<bool> EditarSensorAsync(SensorMOD area)
        {
            return await _sensorDAL.EditarSensorAsync(area);
        }

        public async Task<bool> ExcluirSensorAsync(int codigo)
        {
            return await _sensorDAL.ExcluirSensorAsync(codigo);
        }

        public async Task<List<SensorMOD>> ListarSensorAsync(int codigoEquipamento)
        {
            return await _sensorDAL.ListarSensorAsync(codigoEquipamento);
        }

        public async Task<bool> DuplicarSensorAsync(int codigo)
        {
            var sensorMod = await _sensorDAL.RetornarSensorAsync(codigo);

            sensorMod.Nome += " (Cópia)";

            return await _sensorDAL.CadastrarSensorAsync(sensorMod);
        }

        public async Task<PaginacaoMOD<SensorMOD>> ListarSensorPaginadoAsync(int codigoEquipamento, int pagina, int itensPorPagina)
        {
            var qtdePaginas = await _sensorDAL.RetornarQuantidadePaginaSensorAsync(codigoEquipamento, itensPorPagina);
            pagina = Math.Min(pagina, qtdePaginas);

            return new PaginacaoMOD<SensorMOD>
            {
                Pagina = pagina,
                QtdePaginas = qtdePaginas,
                ItensPorPagina = itensPorPagina,
                Lista = await _sensorDAL.ListarSensorAsync(codigoEquipamento, pagina, itensPorPagina)
            };
        }
    }
}
