using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoramentoSensores.MOD;

namespace MonitoramentoSensores.DAL.Interfaces
{
    public interface ISensorDAL
    {
        Task<SensorMOD> RetornarSensorAsync(int codigo);
        Task<bool> CadastrarSensorAsync(SensorMOD sensor);
        Task<bool> EditarSensorAsync(SensorMOD area);
        Task<bool> ExcluirSensorAsync(int codigo);
        Task<List<SensorMOD>> ListarSensorAsync(int codigoEquipamento);
        Task<int> RetornarQuantidadePaginaSensorAsync(int codigoEquipamento, int itensPorPagina);
        Task<List<SensorMOD>> ListarSensorAsync(int codigoEquipamento, int pagina, int itensPorPagina);
        Task<bool> ReiniciarSimulacaoAsync();
    }
}
