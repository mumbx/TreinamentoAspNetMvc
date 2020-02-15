using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using EditoraMumbs.WebMvc.Utils;

namespace EditoraMumbs.WebMvc.Controllers
{
    public class InicioController : Controller
    {
        //

        // GET: Inicio
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contato()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contato(FormCollection form)
        {
            try { 
            var emailRemetente = "antonioletic@gmail.com";
            var nomeRemetente = form["Nome"];
            var emailDestinatario = form["Email"];
            var assunto = form["Assunto"];
            var mensagem = form["Mensagem"];
           
            //Vamos a classe de envio de email
            EnviarEmail enviarEmail = new EnviarEmail(emailRemetente, nomeRemetente, emailDestinatario, mensagem, assunto);

                ViewBag.MensagemEnviada = "Mensagem enviada com sucesso";
                ViewData["MensagemEndiada"] = "Email enviado";
                TempData["Mensagem"] = "Seu email foi enviado";
            }
            catch
            {
                throw;
            }


            return View();
        }
   }        
}