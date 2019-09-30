using MonitoramentoSensores.MOD;
using MonitoramentoSensores.Models.Equipamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Sensor
{
    public class SensorModel
    {
        public int Codigo { get; set; }

        public int CodigoEquipamento { get; set; }

        public EquipamentoModel Equipamento { get; set; }

        [Display(Name = "Endereço")]
        [Required(ErrorMessage = "Preencha o campo do endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome")]
        public string Nome { get; set; }

        //public Status Status { get; set; }
        public int Status { get; set; }

        public int Ordem { get; set; }

        public bool Ativo { get; set; }

        public SensorModel()
        { }

        public SensorModel(SensorMOD mod)
        {
            Codigo = mod.Codigo;
            CodigoEquipamento = mod.CodigoMSEquipamento;
            Endereco = mod.Endereco;
            Nome = mod.Nome;
            Status = mod.Status;
            Ordem = mod.Ordem;
            Ativo = mod.Ativo;
        }

        public SensorMOD ToMOD()
        {
            return new SensorMOD
            {
                Codigo = Codigo,
                CodigoMSEquipamento = CodigoEquipamento,
                Endereco = Endereco,
                Nome = Nome,
                Status = Status,
                Ordem = Ordem,
                Ativo = Ativo
            };
        }
    }
}