using MonitoramentoSensores.MOD;
using MonitoramentoSensores.Models.Area;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Planta
{
    public class PlantaModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome")]
        public string Nome { get; set; }

        [Display(Name="País")]
        [MinLength(2, ErrorMessage = "Selecione um país")]
        public string Pais { get; set; }


        [Required(ErrorMessage = "Preencha o campo do CEP")]
        public string CEP { get; set; }

        //É GAMBIARRA MAS FOI O UNICO JEITO QUE EU ENCONTREI PRA VERIFICAR SE ESTA TRUE (4 CHARS) E NÃO FALSE (5 CHARS)
        [MaxLength(4, ErrorMessage = "Informe um CEP válido")]
        public string CEPValido { get; set; }

        [Display(Name="Número")]
        [Required(ErrorMessage = "Preencha o campo do número")]
        public int Numero { get; set; }

        public bool Ativo { get; set; }

        public List<AreaModel> ListaArea { get; set; }
        public PaginacaoModel<AreaModel> PaginacaoArea { get; set; }

        public PlantaModel()
        { }

        public PlantaModel(PlantaMOD mod)
        {
            Codigo = mod.Codigo;
            Nome = mod.Nome;
            Pais = mod.Pais;
            CEP = mod.CEP;
            Numero = mod.Numero;
            Ativo = mod.Ativo;
            ListaArea = mod.ListaArea?.Select(s => new AreaModel(s)).ToList();
        }

        public PlantaMOD ToMOD()
        {
            return new PlantaMOD
            {
                Codigo = Codigo,
                Nome = Nome,
                Pais = Pais,
                CEP = CEP,
                Numero = Numero,
                Ativo = Ativo,
                ListaArea = ListaArea?.Select(s => s.ToMOD()).ToList()
            };
        }
    }
}