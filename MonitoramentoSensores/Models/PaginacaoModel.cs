using MonitoramentoSensores.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoramentoSensores.Models
{
    public class PaginacaoModel<T>
    {
        public int Pagina { get; set; }
        public int QtdePaginas { get; set; }
        public int ItensPorPagina { get; set; }
        public List<T> Lista { get; set; }

        public PaginacaoModel()
        { }

        //public PaginacaoModel(PaginacaoMOD<T> mod)
        //{
        //    Pagina = mod.Pagina;
        //    QtdePaginas = mod.QtdePaginas;
        //    ItensPorPagina = mod.ItensPorPagina;
        //    Lista = mod.Lista.Select(c => Activator.CreateInstance<T>()).ToList();
        //}

        //public PaginacaoMOD<T> ToMOD()
        //{
        //    return new PaginacaoMOD<T>
        //    {
        //        Pagina = Pagina,
        //        QtdePaginas = QtdePaginas,
        //        ItensPorPagina = ItensPorPagina,
        //        Lista = Lista.Select(c => c).ToList()
        //    };
        //}
    }
}