using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            ML.Usuario usuario= new ML.Usuario();
            if (result.Correct)
            {
                usuario.UsuarioList = result.Objects;
                return View(usuario);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario==null)
            {
                return View(usuario);
            }
            else
            {

                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar los datos del usuario";
                }
                return View(usuario);

            }
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (usuario.IdUsuario == 0)
            {
                ML.Result result = BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Mensaje = "No ha registrado el usuario" + result.Message;
                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            ML.Result result = BL.Usuario.Login(Email);
            ML.Usuario usuario= new ML.Usuario();

            usuario=(ML.Usuario)result.Object;
            if (result.Correct)
            {

                if (usuario.Email == Email && usuario.Password == Password)
                {
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Message = "email o password incorrectos ";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "email o password incorrectos ";
                return PartialView("ModalLogin");
            }
        }
    }
}