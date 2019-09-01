using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models.Usuario
{
    public class UsuarioModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Preencha o campo do nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo da senha")]
        public string Senha { get; set; }

        public int HashSenha { get; set; }

        public DateTime DataHoraCadastro { get; set; }

        public UsuarioModel() { }

        public UsuarioModel(UsuarioMOD mod)
        {
            Codigo = mod.Codigo;
            Nome = mod.Nome;
            HashSenha = mod.HashSenha;
            DataHoraCadastro = mod.DataHoraCadastro;
        }

        public UsuarioMOD ToMOD()
        {
            return new UsuarioMOD
            {
                Codigo = Codigo,
                Nome = Nome,
                HashSenha = Senha.GetHashCode(),
                DataHoraCadastro = DataHoraCadastro
            };
        }
    }
}