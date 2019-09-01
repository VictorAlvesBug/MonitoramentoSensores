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
    public class SensorDAL : ISensorDAL
    {
        public async Task<SensorMOD> RetornarSensorAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSSensor
                                    WHERE
                                        Codigo = @codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<SensorMOD>(query, new { codigo });
            }
        }

        public async Task<bool> CadastrarSensorAsync(SensorMOD sensor)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    DECLARE @LastOrdem AS INT = (SELECT 
                                                MAX(Ordem)
                                            FROM 
                                                MSSensor 
                                            WHERE 
                                                Ativo = 1 
                                                AND CodigoMSEquipamento = @CodigoMSEquipamento);

                                    INSERT INTO
                                        MSSensor
                                        (CodigoMSEquipamento, Nome, Endereco, Ordem, Ativo)
                                    VALUES
                                        (@CodigoMSEquipamento,
                                        @Nome, 
                                        @Endereco, 
                                        (
                                            CASE WHEN @LastOrdem IS NOT NULL THEN @LastOrdem+1 ELSE 1 END
                                        ), 
                                        1)";
                #endregion

                return await connection.ExecuteAsync(insert, sensor) > 0;
            }
        }

        public async Task<bool> EditarSensorAsync(SensorMOD sensor)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSSensor
                                    SET
                                        Nome = @Nome,
                                        Endereco = @Endereco
                                    WHERE
                                        Codigo = @Codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.ExecuteAsync(update, sensor) > 0;
            }
        }

        public async Task<bool> ExcluirSensorAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSSensor
                                    SET
                                        Ativo = 0
                                    WHERE
                                        Codigo = @codigo";
                #endregion

                return await connection.ExecuteAsync(update, new { codigo }) > 0;
            }
        }

        public async Task<List<SensorMOD>> ListarSensorAsync(int codigoMSEquipamento)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSSensor
                                    WHERE
                                        CodigoMSEquipamento = @codigoMSEquipamento
                                        AND Ativo = 1
                                    ORDER BY 
                                        Ordem";
                #endregion

                return (await connection.QueryAsync<SensorMOD>(query, new { codigoMSEquipamento })).ToList();
            }
        }
    }
}
