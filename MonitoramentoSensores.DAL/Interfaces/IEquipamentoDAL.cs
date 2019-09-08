using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitoramentoSensores.MOD;

namespace MonitoramentoSensores.DAL.Interfaces
{
    public interface IEquipamentoDAL
    {
        Task<EquipamentoMOD> RetornarEquipamentoAsync(int codigo);
        Task<bool> CadastrarEquipamentoAsync(EquipamentoMOD equipamento);
        Task<bool> EditarEquipamentoAsync(EquipamentoMOD equipamento);
        Task<bool> ExcluirEquipamentoAsync(int codigo);
        Task<List<EquipamentoMOD>> ListarEquipamentoAsync(int codigoArea);
        Task<List<EquipamentoMOD>> ListarEquipamentoAsync(int codigoArea, int pagina, int itensPorPagina);
        Task<int> CadastrarEquipamentoRetornarCodigoAsync(EquipamentoMOD equipamento);
        Task<int> RetornarQuantidadePaginaEquipamentoAsync(int codigoArea, int itensPorPagina);
    }
}
