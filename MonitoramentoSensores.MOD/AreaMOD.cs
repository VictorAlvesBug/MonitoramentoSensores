using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.MOD
{
    public class AreaMOD
    {
        public int Codigo { get; set; }
        public int CodigoMSPlanta { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
        public List<EquipamentoMOD> ListaEquipamento { get; set; }
    }
}
