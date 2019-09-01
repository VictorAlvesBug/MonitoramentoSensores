using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL.Interfaces
{
    public interface ILoginBLL
    {
        Task<Login> CadastrarAsync(UsuarioMOD usuario);
        //Task<List<AreaMOD>> ListarAreaAsync(int codigoPlanta);
        Task<Login> EntrarAsync(UsuarioMOD usuario);
        Task<UsuarioMOD> RetornarUsuarioAsync(UsuarioMOD usuario);
        Task<bool> SairAsync();
    }
}
