using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL.Interfaces
{
    public interface IEquipamentoBLL
    {
        Task<bool> CadastrarEquipamentoAsync(EquipamentoMOD equipamento);
        Task<List<EquipamentoMOD>> ListarEquipamentoAsync(int codigoArea);
        Task<EquipamentoMOD> RetornarEquipamentoAsync(int codigo);
        Task<bool> EditarEquipamentoAsync(EquipamentoMOD equipamento);
        Task<bool> ExcluirEquipamentoAsync(int codigo);
        Task<bool> DuplicarEquipamentoAsync(int codigo);
    }
}
