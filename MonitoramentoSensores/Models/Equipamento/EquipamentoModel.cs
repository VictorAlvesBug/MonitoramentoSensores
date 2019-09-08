using MonitoramentoSensores.MOD;
using MonitoramentoSensores.Models.Area;
using MonitoramentoSensores.Models.Sensor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Equipamento
{
    public class EquipamentoModel
    {
        public int Codigo { get; set; }

        public int CodigoArea { get; set; }

        public AreaModel Area { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome")]
        public string Nome { get; set; }

        public int Ordem { get; set; }

        public bool Ativo { get; set; }

        public List<SensorModel> ListaSensor { get; set; }
        public PaginacaoModel<SensorModel> PaginacaoSensor { get; set; }

        public EquipamentoModel()
        { }

        public EquipamentoModel(EquipamentoMOD mod)
        {
            Codigo = mod.Codigo;
            CodigoArea = mod.CodigoMSArea;
            Nome = mod.Nome;
            Ordem = mod.Ordem;
            Ativo = mod.Ativo;
            ListaSensor = mod.ListaSensor?.Select(s => new SensorModel(s)).ToList();
        }

        public EquipamentoMOD ToMOD()
        {
            return new EquipamentoMOD
            {
                Codigo = Codigo,
                CodigoMSArea = CodigoArea,
                Nome = Nome,
                Ordem = Ordem,
                Ativo = Ativo,
                ListaSensor = ListaSensor?.Select(s => s.ToMOD()).ToList()
            };
        }
    }
}