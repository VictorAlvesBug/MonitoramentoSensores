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
    public class EquipamentoBLL : IEquipamentoBLL
    {
        private IEquipamentoDAL _equipamentoDAL;
        private ISensorDAL _sensorDAL;

        public EquipamentoBLL(IEquipamentoDAL equipamentoDAL,ISensorDAL sensorDAL)
        {
            _equipamentoDAL = equipamentoDAL;
            _sensorDAL = sensorDAL;
        }

        public async Task<EquipamentoMOD> RetornarEquipamentoAsync(int codigo)
        {
            return await _equipamentoDAL.RetornarEquipamentoAsync(codigo);
        }

        public async Task<bool> CadastrarEquipamentoAsync(EquipamentoMOD equipamento)
        {
            return await _equipamentoDAL.CadastrarEquipamentoAsync(equipamento);
        }

        public async Task<bool> EditarEquipamentoAsync(EquipamentoMOD equipamento)
        {
            return await _equipamentoDAL.EditarEquipamentoAsync(equipamento);
        }

        public async Task<bool> ExcluirEquipamentoAsync(int codigo)
        {
            return await _equipamentoDAL.ExcluirEquipamentoAsync(codigo);
        }

        public async Task<List<EquipamentoMOD>> ListarEquipamentoAsync(int codigoArea)
        {
            return await _equipamentoDAL.ListarEquipamentoAsync(codigoArea);
        }

        public async Task<bool> DuplicarEquipamentoAsync(int codigo)
        {
            var equipamento = await _equipamentoDAL.RetornarEquipamentoAsync(codigo);
            equipamento.Nome += " (Cópia)";

            var listaSensor = await _sensorDAL.ListarSensorAsync(equipamento.Codigo);

            var codigoNovaEquipamento = await _equipamentoDAL.CadastrarEquipamentoRetornarCodigoAsync(equipamento);

            if (codigoNovaEquipamento == 0)
                return false;

            foreach (var sensor in listaSensor)
            {
                sensor.CodigoMSEquipamento = codigoNovaEquipamento;
                var cadastrou = await _sensorDAL.CadastrarSensorAsync(sensor);

                if (!cadastrou)
                    return false;
            }

            return true;
        }

        public async Task<PaginacaoMOD<EquipamentoMOD>> ListarEquipamentoPaginadoAsync(int codigoArea, int pagina, int itensPorPagina)
        {
            var qtdePaginas = await _equipamentoDAL.RetornarQuantidadePaginaEquipamentoAsync(codigoArea, itensPorPagina);
            pagina = Math.Min(pagina, qtdePaginas);

            return new PaginacaoMOD<EquipamentoMOD>
            {
                Pagina = pagina,
                QtdePaginas = qtdePaginas,
                ItensPorPagina = itensPorPagina,
                Lista = await _equipamentoDAL.ListarEquipamentoAsync(codigoArea, pagina, itensPorPagina)
            };
        }
    }
}
