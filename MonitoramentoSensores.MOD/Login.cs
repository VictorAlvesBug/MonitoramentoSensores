using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.MOD
{
    public enum Login
    {
        ErroAoEfetuarLogin = 0,
        UsuarioInvalido = 1,
        SenhaInvalida = 2,
        Logado = 3
    }
}