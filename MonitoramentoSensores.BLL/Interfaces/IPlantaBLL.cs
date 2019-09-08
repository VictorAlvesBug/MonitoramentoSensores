using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL.Interfaces
{
    public interface IPlantaBLL
    {
        Task<bool> CadastrarPlantaAsync(PlantaMOD planta);
        Task<List<PlantaMOD>> ListarPlantaAsync();
        Task<PlantaMOD> RetornarPlantaAsync(int codigo);
        Task<bool> EditarPlantaAsync(PlantaMOD planta);
        Task<bool> ExcluirPlantaAsync(int codigo);
        Task<bool> DuplicarPlantaAsync(int codigo);
        Task<PaginacaoMOD<PlantaMOD>> ListarPlantaPaginadaAsync(int pagina, int itensPorPagina);
    }
}
