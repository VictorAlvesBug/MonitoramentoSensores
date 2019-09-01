using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.DAL.Interfaces;
using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.BLL
{
    public class LoginBLL : ILoginBLL
    {
        private ILoginDAL _loginDAL;

        public LoginBLL(ILoginDAL loginDAL)
        {
            _loginDAL = loginDAL;
        }

        public async Task<Login> CadastrarAsync(UsuarioMOD usuario)
        {
            return await _loginDAL.CadastrarAsync(usuario);
        }

        public async Task<Login> EntrarAsync(UsuarioMOD usuario)
        {
            return await _loginDAL.EntrarAsync(usuario);
        }

        public async Task<UsuarioMOD> RetornarUsuarioAsync(UsuarioMOD usuario)
        {
            return await _loginDAL.RetornarUsuarioAsync(usuario);
        }

        public async Task<bool> SairAsync()
        {
            return await _loginDAL.SairAsync();
        }
    }
}
