using Dapper;
using MonitoramentoSensores.DAL.ConnectionFactories;
using MonitoramentoSensores.DAL.Interfaces;
using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.DAL
{
    public class PlantaDAL : IPlantaDAL
    {
        public async Task<bool> CadastrarPlantaAsync(PlantaMOD planta)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    INSERT INTO
                                        MSPlanta
                                    (Nome, Pais, CEP, Numero, Ativo)
                                        VALUES
                                    (@Nome, @Pais, @CEP, @Numero, 1)";
                #endregion

                return await connection.ExecuteAsync(insert, planta) > 0;
            }
        }

        public async Task<List<PlantaMOD>> ListarPlantaAsync()
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSPlanta
                                    WHERE
                                        Ativo = 1";
                #endregion

                return (await connection.QueryAsync<PlantaMOD>(query)).ToList();
            }
        }

        public async Task<PlantaMOD> RetornarPlantaAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSPlanta
                                    WHERE
                                        Codigo = @codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<PlantaMOD>(query, new { codigo });
            }
        }
        
        public async Task<bool> EditarPlantaAsync(PlantaMOD planta)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSPlanta
                                    SET
                                        Nome = @Nome,
                                        Pais = @Pais,
                                        CEP = @CEP,
                                        Numero = @Numero
                                    WHERE
                                        Codigo = @Codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.ExecuteAsync(update, planta) > 0;
            }
        }

        public async Task<bool> ExcluirPlantaAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSPlanta
                                    SET
                                        Ativo = 0
                                    WHERE
                                        Codigo = @Codigo";
                #endregion

                return await connection.ExecuteAsync(update, new { codigo }) > 0;
            }
        }

        public async Task<int> CadastrarPlantaRetornarCodigoAsync(PlantaMOD planta)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    INSERT INTO
                                        MSPlanta
                                    (Nome, Pais, CEP, Numero, Ativo)
                                        VALUES
                                    (@Nome, @Pais, @CEP, @Numero, 1);
                                    SELECT SCOPE_IDENTITY()";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<int>(insert, planta);
            }
        }

        public async Task<int> RetornarQuantidadePaginaPlantaAsync(int itensPorPagina)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region COUNT
                const string count = @"
                                    SELECT 
                                        CEILING(
                                            (
                                                SELECT
                                                    COUNT(*)
                                                FROM
                                                    MSPlanta
                                                WHERE
                                                    Ativo = 1
                                            )
                                        /CONVERT(FLOAT, @itensPorPagina))";
                #endregion

                return (await connection.QueryFirstOrDefaultAsync<int>(count, new { itensPorPagina }));
            }
        }

        public async Task<List<PlantaMOD>> ListarPlantaAsync(int pagina, int itensPorPagina)
        {
            if (pagina < 1)
                pagina = 1;

            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSPlanta
                                    WHERE
                                        Ativo = 1
                                    ORDER BY 
                                        Nome
                                    OFFSET
                                        (@pagina-1) * @itensPorPagina ROWS
                                    FETCH NEXT
                                        @itensPorPagina
                                    ROWS ONLY";
                #endregion

                return (await connection.QueryAsync<PlantaMOD>(query, new { pagina, itensPorPagina })).ToList();
            }
        }
    }
}
