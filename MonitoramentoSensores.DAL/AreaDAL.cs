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
    public class AreaDAL : IAreaDAL
    {
        public async Task<AreaMOD> RetornarAreaAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                /*
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSArea
                                        LEFT JOIN MSEquipamento ON MSArea.Codigo = MSEquipamento.CodigoMSArea
                                        AND MSEquipamento.Ativo = 1
                                        LEFT JOIN MSSensor ON MSEquipamento.Codigo = MSSensor.CodigoMSEquipamento
                                        AND MSSensor.Ativo = 1
                                    WHERE
                                        MSArea.Codigo = @codigo
                                        AND MSArea.Ativo = 1";
                #endregion

                Dictionary<int, AreaMOD> dicionarioArea = new Dictionary<int, AreaMOD>();
                Dictionary<int, EquipamentoMOD> dicionarioEquipamento = new Dictionary<int, EquipamentoMOD>();

                await connection.QueryAsync<AreaMOD, EquipamentoMOD, SensorMOD, AreaMOD>(query,
                    (area, equipamento, sensor) =>
                    {
                        if (!dicionarioArea.TryGetValue(area.Codigo, out var novaArea))
                        {
                            dicionarioArea.Add(area.Codigo, novaArea = area);
                            novaArea.ListaEquipamento = new List<EquipamentoMOD>();
                        }

                        if (!dicionarioEquipamento.TryGetValue(equipamento.Codigo, out var novaEquipamento))
                        {
                            dicionarioEquipamento.Add(equipamento.Codigo, novaEquipamento = equipamento);
                            novaEquipamento.ListaSensor = new List<SensorMOD>();
                        }

                        novaEquipamento.ListaSensor.Add(sensor);
                        novaArea.ListaEquipamento.Add(novaEquipamento);


                        return null;
                    },
                    new { codigo },
                    splitOn: "Codigo");

                return dicionarioArea.Values.FirstOrDefault();
                */

                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSArea
                                    WHERE
                                        Codigo = @codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<AreaMOD>(query, new { codigo });
            }
        }

        public async Task<bool> CadastrarAreaAsync(AreaMOD area)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    DECLARE @LastOrdem AS INT = (SELECT 
                                                MAX(Ordem)
                                            FROM 
                                                MSArea 
                                            WHERE 
                                                Ativo = 1 
                                                AND CodigoMSPlanta = @CodigoMSPlanta);

                                    INSERT INTO
                                        MSArea
                                        (CodigoMSPlanta, Nome, Ordem, Ativo)
                                    VALUES
                                        (@CodigoMSPlanta, 
                                        @Nome,
                                        (
                                            CASE WHEN @LastOrdem IS NOT NULL THEN @LastOrdem+1 ELSE 1 END
                                        ), 
                                        1)";
                #endregion

                return await connection.ExecuteAsync(insert, area) > 0;
            }
        }

        public async Task<bool> EditarAreaAsync(AreaMOD area)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSArea
                                    SET
                                        Nome = @Nome
                                    WHERE
                                        Codigo = @Codigo
                                        AND Ativo = 1";
                #endregion

                return await connection.ExecuteAsync(update, area) > 0;
            }
        }

        public async Task<bool> ExcluirAreaAsync(int codigo)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region UPDATE
                const string update = @"
                                    UPDATE
                                        MSArea
                                    SET
                                        Ativo = 0
                                    WHERE
                                        Codigo = @codigo";
                #endregion

                return await connection.ExecuteAsync(update, new { codigo }) > 0;
            }
        }

        public async Task<List<AreaMOD>> ListarAreaAsync(int codigoMSPlanta)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region QUERY
                const string query = @"
                                    SELECT
                                        *
                                    FROM
                                        MSArea
                                    WHERE
                                        CodigoMSPlanta = @codigoMSPlanta
                                        AND Ativo = 1
                                    ORDER BY 
                                        Ordem";
                #endregion

                return (await connection.QueryAsync<AreaMOD>(query, new { codigoMSPlanta })).ToList();
            }
        }

        public async Task<int> CadastrarAreaRetornarCodigoAsync(AreaMOD area)
        {
            using (var connection = ConnectionFactory.GetConnection("MyDatabase"))
            {
                #region INSERT
                const string insert = @"
                                    DECLARE @LastOrdem AS INT = (SELECT 
                                                MAX(Ordem)
                                            FROM 
                                                MSArea 
                                            WHERE 
                                                Ativo = 1 
                                                AND CodigoMSPlanta = @CodigoMSPlanta);

                                    INSERT INTO
                                        MSArea
                                        (CodigoMSPlanta, Nome, Ordem, Ativo)
                                    VALUES
                                        (@CodigoMSPlanta, 
                                        @Nome,
                                        (
                                            CASE WHEN @LastOrdem IS NOT NULL THEN @LastOrdem+1 ELSE 1 END
                                        ), 
                                        1);
                                    SELECT SCOPE_IDENTITY()";
                #endregion

                return await connection.QueryFirstOrDefaultAsync<int>(insert, area);
            }
        }

        public async Task<List<AreaMOD>> ListarAreaAsync(int codigoMSPlanta, int pagina, int itensPorPagina)
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
                                        MSArea
                                    WHERE
                                        CodigoMSPlanta = @codigoMSPlanta
                                        AND Ativo = 1
                                    ORDER BY 
                                        Ordem
                                    OFFSET
                                        (@pagina-1) * @itensPorPagina ROWS
                                    FETCH NEXT
                                        @itensPorPagina
                                    ROWS ONLY";
                #endregion

                return (await connection.QueryAsync<AreaMOD>(query, new { codigoMSPlanta, pagina, itensPorPagina })).ToList();
            }
        }

        public async Task<int> RetornarQuantidadePaginaAreaAsync(int codigoMSPlanta, int itensPorPagina)
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
                                                    MSArea
                                                WHERE
                                                    CodigoMSPlanta = @codigoMSPlanta
                                                    AND Ativo = 1
                                            )
                                        /CONVERT(FLOAT, @itensPorPagina))";
                #endregion

                return (await connection.QueryFirstOrDefaultAsync<int>(count, new { codigoMSPlanta, itensPorPagina }));
            }
        }
    }
}
