using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.MOD
{
    public class PaginacaoMOD<T>
    {
        public int Pagina { get; set; }
        public int QtdePaginas { get; set; }
        public int ItensPorPagina { get; set; }
        public List<T> Lista { get; set; }
    }
}
