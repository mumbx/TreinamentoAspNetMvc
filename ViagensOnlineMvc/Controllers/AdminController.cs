using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ViagensOnlineMvc.Db;
using ViagensOnlineMvc.Models;

namespace ViagensOnlineMvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private const string ActionDestinoListagem = "DestinoListagem";
        // GET: Admin
        
        public ActionResult DestinoListagem()
        {
            List<Destino> lista = null;
            using (var db = ObterDbContext())
            {
                lista = db.Destinos.ToList();
            }
            return View(lista);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DestinoNovo()
        {
            return View();
        }

        private string GravarFoto(HttpRequestBase Request)
        {
            string nome = Path.GetFileName(Request.Files[0].FileName);

            string pastaVirtual = "~/imagens";

            string pathVirtual = pastaVirtual + "/" + nome;

            string pathFisico = Request.MapPath(pathVirtual);

            Request.Files[0].SaveAs(pathFisico);

            return nome;
        }

        private ViagensOnLineDb ObterDbContext()
        {
            return new ViagensOnLineDb();
        }

        //
        // Gravar Novo Destino
        //
        [HttpPost]
        public ActionResult DestinoNovo(Destino destino)
        {
            //Se alguma validação falhou...
            if (!ModelState.IsValid)
            {
                return View(destino);
            }
            // Foto é obrigatória
            if (Request.Files.Count == 0 ||
            Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("",
                "É necessário enviar uma Foto");
                return View(destino);
            }
            //Grava
            try
            {
                //Grava a foto e retorna o nome
                destino.Foto = GravarFoto(Request);
                using (var db = ObterDbContext())
                {
                    db.Destinos.Add(destino);
                    db.SaveChanges();
                    return RedirectToAction(ActionDestinoListagem);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(destino);
            }
        }

            [HttpGet]
            public ActionResult DestinoAlterar(int id)
            {
            using (var db = ObterDbContext())
            {
                var destino = db.Destinos.Find(id);
                if (destino != null) { return View(destino); }
            }
            return RedirectToAction(ActionDestinoListagem);
            }

        [HttpPost]
        public ActionResult DestinoAlterar(Destino destino)
        {
            //Se o modelo é válido..
            if (ModelState.IsValid)
            {
                using (var db = ObterDbContext())
                {
                    //Obtém o original
                    var destinoOriginal = db.Destinos.Find(destino.DestinoId);
                    //Se encontrou, altera o original
                    if (destinoOriginal != null)
                    {
                        destinoOriginal.Nome = destino.Nome;
                        destinoOriginal.Cidade = destino.Cidade;
                        destinoOriginal.Pais = destino.Pais;
                        //Altera a imagem apenas se enviou outra
                        if (Request.Files.Count > 0 &&
                        Request.Files[0].ContentLength > 0 ) {
                            destinoOriginal.Foto = GravarFoto(Request);
                        }
                        //Grava
                        db.SaveChanges();
                        return RedirectToAction(ActionDestinoListagem);
                    }
                }
            }
            //Se chegou aqui e não foi redirecionado, é porque
            // houve algum problema
            return View(destino);
        }

        [HttpGet]
        public ActionResult DestinoExcluir(int id)
        {
            using (var db = ObterDbContext())
            {
                var destino = db.Destinos.Find(id);
                if (destino != null)
                {
                    return View(destino);
                }
            }
            return RedirectToAction(ActionDestinoListagem);
        }
        //
        // Excluir
        //
        [HttpPost]
        public ActionResult DestinoExcluir(int id,
        FormCollection form)
        {
            using (var db = ObterDbContext())
            {
                var destino = db.Destinos.Find(id);
                if (destino != null)
                {
                    db.Destinos.Remove(destino);
                    db.SaveChanges();
                }
            }
            return RedirectToAction(ActionDestinoListagem);
        }

        // Login
        //
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string nome, string senha)
        {
            if (string.IsNullOrEmpty(nome))
            {
                ViewBag.Mensagem = "Digite o nome";
                return View();
            }
            if (string.IsNullOrEmpty(senha))
            {
                ViewBag.Mensagem = "Digite o senha";
                return View();
            }
            if (nome != "admin" && senha != "admin")
            {
                @ViewBag.Mensagem = "Usuário ou senha inválida";
                return View();
            }
            //Um Array de claims. Claim é uma declaração que
            // o usuário faz. Nesse caso, são duas: Ele se chama
            // Administrador e pertence ao grupo admin
            Claim[] claims = new Claim[2];
            claims[0] = new Claim(ClaimTypes.Name, "Administrador");
            claims[1] = new Claim(ClaimTypes.Role, "admin");
            //Nome para identificar
            string nomeAutenticacao = "AppViagensOnLineCookie";
            //Identidade
            ClaimsIdentity identity =
            new ClaimsIdentity(claims, nomeAutenticacao);
            //Autentica
            Request.GetOwinContext().Authentication.SignIn(identity);
            //Redireciona para a pasta destinos
            return RedirectToAction(ActionDestinoListagem);
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();

            Session.Abandon();

            return View("Login");
        }
      
    }

    }
