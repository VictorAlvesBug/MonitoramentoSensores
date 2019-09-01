using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.DAL.Interfaces
{
    public interface IPlantaDAL
    {
        Task<List<PlantaMOD>> ListarPlantaAsync();
        Task<bool> CadastrarPlantaAsync(PlantaMOD planta);
        Task<PlantaMOD> RetornarPlantaAsync(int codigo);
        Task<bool> EditarPlantaAsync(PlantaMOD planta);
        Task<bool> ExcluirPlantaAsync(int codigo);
        Task<int> CadastrarPlantaRetornarCodigoAsync(PlantaMOD planta);
    }
}
