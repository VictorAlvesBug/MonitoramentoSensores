using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL.Interfaces
{
    public interface ISensorBLL
    {
        Task<bool> CadastrarSensorAsync(SensorMOD area);
        Task<List<SensorMOD>> ListarSensorAsync(int codigoEquipamento);
        Task<SensorMOD> RetornarSensorAsync(int codigo);
        Task<bool> EditarSensorAsync(SensorMOD sensor);
        Task<bool> ExcluirSensorAsync(int codigo);
        Task<bool> DuplicarSensorAsync(int codigo);
        Task<PaginacaoMOD<SensorMOD>> ListarSensorPaginadoAsync(int codigoEquipamento, int pagina, int itensPorPagina);
        Task<bool> ReiniciarSimulacaoAsync();
    }
}
