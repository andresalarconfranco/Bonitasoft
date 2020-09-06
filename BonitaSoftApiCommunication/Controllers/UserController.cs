using BonitaSoftApiBusiness;
using BonitaSoftApiBusiness.Security;
using BonitaSoftApiBusiness.User;
using BonitaSoftApiEntties.Entities;
using BonitaSoftApiEntties.Request;
using BonitaSoftApiEntties.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace BonitaSoftApiCommunication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Obtiene el id del usuario a través de su nombre
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario a consultar</param>
        /// <returns></returns>
        [Route("GetUserById")]
        [HttpGet]
        public GenericResponse<string> GetUserId([FromBody] RequestUser requestUser)
        {
            try
            {
                RequestAuthorization requestAuthorization = new RequestAuthorization()
                {
                    Password = Request.Headers.ToList().FirstOrDefault(x => x.Key == "pws").Value.ToString(),
                    UserName = Request.Headers.ToList().FirstOrDefault(x => x.Key == "username").Value.ToString()
                };

                var secure = this.Authorize(requestAuthorization);

                if (secure.Data.SessionId != null)
                {
                    var idUsuario = this.GetUserIdByName(requestUser, secure.Data);

                    this.CloseSession(secure.Data);

                    return Mapper<string>.MapGenericResponse(idUsuario.Data, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"Id for User: { requestUser.UserName }", string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse(string.Empty, HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), "Wrong username or password", "Verify credentials");
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("ERROR", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        #region Auxiliares
        /// <summary>
        /// Autentica un usuario
        /// </summary>
        /// <param name="requestAuthorization">Request authorization</param>
        /// <returns>Contexto de seguridad</returns>
        private GenericResponse<SecurityContext> Authorize(RequestAuthorization requestAuthorization)
        {
            Authorizator authorizator = new Authorizator();

            return authorizator.Authenticate(requestAuthorization);
        }

        /// <summary>
        /// Obtiene el Id del usuario a través de su nombre
        /// </summary>
        /// <param name="requestUser">Contexto usuario</param>
        /// <param name="security">Contexto de seguridad</param>
        /// <returns></returns>
        private GenericResponse<string> GetUserIdByName(RequestUser requestUser, SecurityContext security)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            return usuarioManager.GetUserIdByname(requestUser.UserName, security);
        }

        /// <summary>
        /// Cierra la sesion
        /// </summary>
        /// <param name="security">Contexto de seguridad</param>
        private void CloseSession(SecurityContext security)
        {
            Authorizator authorizator = new Authorizator();

            authorizator.CloseSession(security);
        }
        #endregion
    }
}