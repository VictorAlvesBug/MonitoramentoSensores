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
    public class AreaBLL : IAreaBLL
    {
        private IAreaDAL _areaDAL;
        private IEquipamentoDAL _equipamentoDAL;
        private ISensorDAL _sensorDAL;

        public AreaBLL(IAreaDAL areaDAL, IEquipamentoDAL equipamentoDAL, ISensorDAL sensorDAL)
        {
            _areaDAL = areaDAL;
            _equipamentoDAL = equipamentoDAL;
            _sensorDAL = sensorDAL;
        }

        public async Task<AreaMOD> RetornarAreaAsync(int codigo)
        {
            return await _areaDAL.RetornarAreaAsync(codigo);
        }

        public async Task<bool> CadastrarAreaAsync(AreaMOD area)
        {
            return await _areaDAL.CadastrarAreaAsync(area);
        }

        public async Task<bool> EditarAreaAsync(AreaMOD area)
        {
            return await _areaDAL.EditarAreaAsync(area);
        }

        public async Task<bool> ExcluirAreaAsync(int codigo)
        {
            return await _areaDAL.ExcluirAreaAsync(codigo);
        }

        public async Task<List<AreaMOD>> ListarAreaAsync(int codigoPlanta)
        {
            return await _areaDAL.ListarAreaAsync(codigoPlanta);
        }

        public async Task<bool> DuplicarAreaAsync(int codigo)
        {
            var area = await _areaDAL.RetornarAreaAsync(codigo);
            area.Nome += " (Cópia)";

            var listaEquipamento = await _equipamentoDAL.ListarEquipamentoAsync(area.Codigo);

            var codigoNovaArea = await _areaDAL.CadastrarAreaRetornarCodigoAsync(area);

            if (codigoNovaArea == 0)
                return false;

            foreach (var equipamento in listaEquipamento)
            {
                equipamento.CodigoMSArea = codigoNovaArea;
                var codigoNovaEquipamento = await _equipamentoDAL.CadastrarEquipamentoRetornarCodigoAsync(equipamento);

                if(codigoNovaEquipamento == 0)
                    return false;

                var listaSensor = await _sensorDAL.ListarSensorAsync(equipamento.Codigo);

                foreach (var sensor in listaSensor)
                {
                    sensor.CodigoMSEquipamento = codigoNovaEquipamento;
                    var cadastrou = await _sensorDAL.CadastrarSensorAsync(sensor);

                    if (!cadastrou)
                        return false;
                }
            }

            return true;
        }

        public async Task<PaginacaoMOD<AreaMOD>> ListarAreaPaginadaAsync(int codigoPlanta, int pagina, int itensPorPagina)
        {
            var qtdePaginas = await _areaDAL.RetornarQuantidadePaginaAreaAsync(codigoPlanta, itensPorPagina);
            pagina = Math.Min(pagina, qtdePaginas);

            return new PaginacaoMOD<AreaMOD>
            {
                Pagina = pagina,
                QtdePaginas = qtdePaginas,
                ItensPorPagina = itensPorPagina,
                Lista = await _areaDAL.ListarAreaAsync(codigoPlanta, pagina, itensPorPagina)
            };
        }
    }
}
