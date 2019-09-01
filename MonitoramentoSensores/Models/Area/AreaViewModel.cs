using MonitoramentoSensores.Models.Planta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Area
{
    public class AreaViewModel
    {
        public AreaModel Area { get; set; }
        public PlantaModel Planta { get; set; }
        public List<AreaModel> ListaArea { get; set; }
    }
}