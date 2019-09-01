using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL.Interfaces
{
    public interface IAreaBLL
    {
        Task<bool> CadastrarAreaAsync(AreaMOD area);
        Task<List<AreaMOD>> ListarAreaAsync(int codigoPlanta);
        Task<AreaMOD> RetornarAreaAsync(int codigo);
        Task<bool> EditarAreaAsync(AreaMOD area);
        Task<bool> ExcluirAreaAsync(int codigo);
        Task<bool> DuplicarAreaAsync(int codigo);
        Task<PaginacaoMOD<AreaMOD>> ListarAreaPaginadaAsync(int codigoPlanta, int pagina, int itensPorPagina);
    }
}
