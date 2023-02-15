using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EIgnacioPuntosNetEntities context = new DL.EIgnacioPuntosNetEntities())
                {
                    var query = context.UsuarioGetAll().ToList();
                    result.Objects = new List<object>();

                    if (query!=null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario= new ML.Usuario();

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno= obj.ApellidoPaterno;
                            usuario.ApellidoMaterno= obj.ApellidoMaterno;
                            usuario.FechaNacimiento= obj.FechaNacimiento;
                            usuario.Email=obj.Email;
                            usuario.Password=obj.Password;
                            result.Objects.Add(usuario);
                        }
                    }
                    result.Correct = true; 
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EIgnacioPuntosNetEntities context = new DL.EIgnacioPuntosNetEntities())
                {
                    var query  = context.UsuarioGetById(IdUsuario).SingleOrDefault();
                    result.Objects = new List<object>();

                    if (query!=null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        result.Object= usuario;
                    }

                }

                result.Correct = true;
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result= new ML.Result();

            try
            {
                using (DL.EIgnacioPuntosNetEntities context= new DL.EIgnacioPuntosNetEntities())
                {
                    var query = context.UsuarioAdd(usuario.Nombre,usuario.ApellidoPaterno,usuario.ApellidoMaterno,usuario.FechaNacimiento,usuario.Email,usuario.Password);
                    result.Objects=new List<object> ();
                    if (query>0)
                    {
                        result.Message = "Se ingresaron los datos correctament";
                    }
                }
                result.Correct= true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static ML.Result Login(string Email)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            try
            {
                using (DL.EIgnacioPuntosNetEntities context = new DL.EIgnacioPuntosNetEntities())
                {


                    var query=context.LoginUser(Email).SingleOrDefault();
                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        result.Object = usuario;
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
