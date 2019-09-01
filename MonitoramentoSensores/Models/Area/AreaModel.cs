using MonitoramentoSensores.MOD;
using MonitoramentoSensores.Models.Planta;
using MonitoramentoSensores.Models.Equipamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Area
{
    public class AreaModel
    {
        public int Codigo { get; set; }

        public int CodigoPlanta { get; set; }

        public PlantaModel Planta { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome")]
        public string Nome { get; set; }

        public int Ordem { get; set; }

        public bool Ativo { get; set; }

        public List<EquipamentoModel> ListaEquipamento { get; set; }

        public AreaModel()
        { }

        public AreaModel(AreaMOD mod)
        {
            Codigo = mod.Codigo;
            CodigoPlanta = mod.CodigoMSPlanta;
            Nome = mod.Nome;
            Ordem = mod.Ordem;
            Ativo = mod.Ativo;
            ListaEquipamento = mod.ListaEquipamento?.Select(s => new EquipamentoModel(s)).ToList();
        }

        public AreaMOD ToMOD()
        {
            return new AreaMOD
            {
                Codigo = Codigo,
                CodigoMSPlanta = CodigoPlanta,
                Nome = Nome,
                Ordem = Ordem,
                Ativo = Ativo,
                ListaEquipamento = ListaEquipamento?.Select(s => s.ToMOD()).ToList()
            };
        }
    }
}
