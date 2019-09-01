using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoramentoSensores.MOD;

namespace MonitoramentoSensores.DAL.Interfaces
{
    public interface IAreaDAL
    {
        Task<AreaMOD> RetornarAreaAsync(int codigo);
        Task<bool> CadastrarAreaAsync(AreaMOD area);
        Task<bool> EditarAreaAsync(AreaMOD area);
        Task<bool> ExcluirAreaAsync(int codigo);
        Task<List<AreaMOD>> ListarAreaAsync(int codigoPlanta);
        Task<List<AreaMOD>> ListarAreaAsync(int codigoPlanta, int pagina, int itensPorPagina);
        Task<int> CadastrarAreaRetornarCodigoAsync(AreaMOD area);
        Task<int> RetornarQuantidadePaginaAreaAsync(int codigoPlanta, int itensPorPagina);
    }
}
