using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.MOD;
using MonitoramentoSensores.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MonitoramentoSensores.Controllers
{
    public class LoginController : Controller
    {
        private ILoginBLL _loginBLL;

        public LoginController(ILoginBLL loginBLL)
        {
            _loginBLL = loginBLL;
        }

        public async Task<ActionResult> Index()
        {
            if (Session["CodigoUsuario"] != null)
                return RedirectToAction("Index", "Planta");

            return View(new UsuarioModel());
        }

        public async Task<ActionResult> Entrar(UsuarioModel usuario)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Preencha todos os campos"
                }, JsonRequestBehavior.AllowGet);

            switch (await _loginBLL.EntrarAsync(usuario.ToMOD()))
            {
                case Login.UsuarioInvalido:
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Nome de usuário e senha inválidos"
                }, JsonRequestBehavior.AllowGet);

                case Login.SenhaInvalida:
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Senha inválida"
                }, JsonRequestBehavior.AllowGet);

                case Login.Logado:
                usuario = new UsuarioModel(await _loginBLL.RetornarUsuarioAsync(usuario.ToMOD()));
                Session.Add("CodigoUsuario", usuario.Codigo);
                Session.Add("NomeUsuario", usuario.Nome);
                return Json(new
                {
                    Sucesso = true,
                    Mensagem = "Login efetuado com sucesso"
                }, JsonRequestBehavior.AllowGet);

                default:
                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Erro ao efetuar login"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Sair(UsuarioModel usuario)
        {
            Session.Clear();

            return View("Index", new UsuarioModel());
        }
    }
}