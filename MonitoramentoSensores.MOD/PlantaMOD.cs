using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.MOD
{
    public class PlantaMOD
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string CEP { get; set; }
        public int Numero { get; set; }
        public bool Ativo { get; set; }
        public List<AreaMOD> ListaArea { get; set; }
    }
}
