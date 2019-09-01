using Dapper;
using MonitoramentoSensores.DAL.Interfaces;
using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.DAL
{
    public class LoginDAL : ILoginDAL
    {
        public async Task<Login> CadastrarAsync(UsuarioMOD usuario)
        {
            throw new NotImplementedException();
        }
        public async Task<UsuarioMOD> RetornarUsuarioAsync(UsuarioMOD usuario)
        {
            using (var connection = ConnectionFactories.ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                        SELECT
                                            *
                                        FROM
                                            MSUsuario
                                        WHERE
                                            Nome = @Nome
                                            AND HashSenha = @HashSenha";
                #endregion

                return (await connection.QueryFirstOrDefaultAsync<UsuarioMOD>(query, usuario));
            }
        }

        public async Task<Login> EntrarAsync(UsuarioMOD usuario)
        {
            using (var connection = ConnectionFactories.ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                        SELECT
                                            *
                                        FROM
                                            MSUsuario
                                        WHERE
                                            Nome = @Nome";
                #endregion

                var listaUsuario = (await connection.QueryAsync<UsuarioMOD>(query, usuario)).ToList();

                if (!listaUsuario.Any())
                    return Login.UsuarioInvalido;

                if (!listaUsuario.Where(c => c.HashSenha == usuario.HashSenha).Any())
                    return Login.SenhaInvalida;

                return Login.Logado;
            }
        }

        public async Task<bool> SairAsync()
        {
            throw new NotImplementedException();
        }
    }
}
