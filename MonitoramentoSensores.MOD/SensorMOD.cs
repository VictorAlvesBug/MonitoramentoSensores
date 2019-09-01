using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.MOD
{
    public class SensorMOD
    {
        public int Codigo { get; set; }
        public int CodigoMSEquipamento { get; set; }
        public string Endereco { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
    }
}
