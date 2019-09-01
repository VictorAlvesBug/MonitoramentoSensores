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
    public class EquipamentoDAL : IEquipamentoDAL
    {
        public async Task<EquipamentoMOD> RetornarEquipamentoAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                /*
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSEquipamento
                                        LEFT JOIN MSSensor ON MSEquipamento.Codigo = MSSensor.CodigoMSEquipamento
                                        AND MSSensor.Ativo = 1
                                    WHERE
                                        MSEquipamento.Codigo = @codigo
                                        AND MSEquipamento.Ativo = 1";
                #endregion

                Dictionary<int, EquipamentoMOD> dicionario = new Dictionary<int, EquipamentoMOD>();

                await connection.QueryAsync<EquipamentoMOD, SensorMOD, EquipamentoMOD>(query,
                    (equipamento, sensor) =>
                    {
                        if (!dicionario.TryGetValue(equipamento.Codigo, out var novaEquipamento))
                        {
                            dicionario.Add(equipamento.Codigo, novaEquipamento = equipamento);
                            novaEquipamento.ListaSensor = new List<SensorMOD>();
                        }

                        novaEquipamento.ListaSensor.Add(sensor);

                        return null;
                    },
                    new { codigo },
                    splitOn: "Codigo");

                return dicionario.Values.FirstOrDefault();
                */

                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSEquipamento
                                    WHERE
                                        Codigo = @codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<EquipamentoMOD>(query, new { codigo });
            }
        }

        public async Task<bool> CadastrarEquipamentoAsync(EquipamentoMOD equipamento)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    DECLARE @LastOrdem AS INT = (SELECT 
                                                MAX(Ordem)
                                            FROM 
                                                MSEquipamento 
                                            WHERE 
                                                Ativo = 1 
                                                AND CodigoMSArea = @CodigoMSArea);

                                    INSERT INTO
                                        MSEquipamento
                                        (CodigoMSArea, Nome, Ordem, Ativo)
                                    VALUES
                                        (@CodigoMSArea,
                                        @Nome,
                                        (
                                            CASE WHEN @LastOrdem IS NOT NULL THEN @LastOrdem+1 ELSE 1 END
                                        ), 
                                        1)";
                #endregion

                return await connection.ExecuteAsync(insert, equipamento) > 0;
            }
        }

        public async Task<bool> EditarEquipamentoAsync(EquipamentoMOD equipamento)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSEquipamento
                                    SET
                                        Nome = @Nome
                                    WHERE
                                        Codigo = @Codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.ExecuteAsync(update, equipamento) > 0;
            }
        }

        public async Task<bool> ExcluirEquipamentoAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSEquipamento
                                    SET
                                        Ativo = 0
                                    WHERE
                                        Codigo = @codigo";
                #endregion

                return await connection.ExecuteAsync(update, new { codigo }) > 0;
            }
        }

        public async Task<List<EquipamentoMOD>> ListarEquipamentoAsync(int codigoMSArea)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                /*
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSEquipamento
                                        LEFT JOIN MSSensor ON MSEquipamento.Codigo = MSSensor.CodigoMSEquipamento
                                        AND MSSensor.Ativo = 1
                                    WHERE
                                        MSEquipamento.CodigoMSArea = @codigoMSArea
                                        AND MSEquipamento.Ativo = 1
                                    ORDER BY 
                                        MSEquipamento.Ordem,
                                        MSSensor.Ordem";
                #endregion

                Dictionary<int, EquipamentoMOD> dicionario = new Dictionary<int, EquipamentoMOD>();

                await connection.QueryAsync<EquipamentoMOD, SensorMOD, EquipamentoMOD>(query,
                    (equipamento, sensor) =>
                    {
                        if (!dicionario.TryGetValue(equipamento.Codigo, out var novaEquipamento))
                        {
                            dicionario.Add(equipamento.Codigo, novaEquipamento = equipamento);
                            novaEquipamento.ListaSensor = new List<SensorMOD>();
                        }

                        novaEquipamento.ListaSensor.Add(sensor);

                        return null;
                    },
                    new { codigoMSArea },
                    splitOn: "Codigo");

                return dicionario.Values.ToList();
                */

                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSEquipamento
                                    WHERE
                                        CodigoMSArea = @codigoMSArea
                                        AND Ativo = 1
                                    ORDER BY 
                                        Ordem";
                #endregion

                return (await connection.QueryAsync<EquipamentoMOD>(query, new { codigoMSArea })).ToList();
            }
        }

        public async Task<int> CadastrarEquipamentoRetornarCodigoAsync(EquipamentoMOD equipamento)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    DECLARE @LastOrdem AS INT = (SELECT 
                                                MAX(Ordem)
                                            FROM 
                                                MSEquipamento 
                                            WHERE 
                                                Ativo = 1 
                                                AND CodigoMSArea = @CodigoMSArea);

                                    INSERT INTO
                                        MSEquipamento
                                        (CodigoMSArea, Nome, Ordem, Ativo)
                                    VALUES
                                        (@CodigoMSArea,
                                        @Nome,
                                        (
                                            CASE WHEN @LastOrdem IS NOT NULL THEN @LastOrdem+1 ELSE 1 END
                                        ), 
                                        1);
                                    SELECT SCOPE_IDENTITY()";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<int>(insert, equipamento);
            }
        }
    }
}
