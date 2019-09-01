using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Equipamento
{
    public class EquipamentoViewModel
    {
        public EquipamentoModel Equipamento { get; set; }
        public List<EquipamentoModel> ListaEquipamento { get; set; }
    }
}