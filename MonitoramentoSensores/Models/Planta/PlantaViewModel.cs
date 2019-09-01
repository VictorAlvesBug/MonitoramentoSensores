using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Planta
{
    public class PlantaViewModel
    {
        public PlantaModel Planta { get; set; }
        public List<PlantaModel> ListaPlanta { get; set; }
    }
}