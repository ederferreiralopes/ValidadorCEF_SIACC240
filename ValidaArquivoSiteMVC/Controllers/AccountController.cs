using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ValidaArquivoSiteMVC.Models;
using Repositorio.Entidades;

namespace ValidaArquivoSiteMVC.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioModel usuario)
        {
            var repo = new UsuarioRepositorio();
            var usuarioDb = repo.ValidarAcesso(usuario.Email, usuario.Senha);

            if (usuarioDb != null)
            {
                FormsAuthenticationTicket authenticationTicket = new FormsAuthenticationTicket(usuario.Email, false, 60);
                string encryptTicket = FormsAuthentication.Encrypt(authenticationTicket);
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                Response.Cookies.Add(authCookie);

                TempData["mensagem"] = " Bem vindo, " + usuarioDb.Nome;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["mensagemErro"] = " Usuario ou senha invalidos! ";
                return View("Index");
            }
            
        }

        public ActionResult Cadastro(UsuarioModel usuario)
        {
            return View("Cadastro");
        }

        [HttpPost]
        public ActionResult Cadastrar(UsuarioModel usuario)
        {
            var repo = new UsuarioRepositorio();
            if (repo.LoginExiste(usuario.Email))            
                TempData["mensagem"] = "Email não disponível";                          
            else
            {                
                repo.Add(new Usuario { Nome = usuario.Nome, Email = usuario.Email, Senha = usuario.Senha, DataCadastro = DateTime.Now });
                TempData["mensagem"] = "Sucesso";
            }

            return View("Cadastro");
        }
        public RedirectToRouteResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
 
            return RedirectToAction("Index");
        }

    }
}
