using BonitaSoftApiBusiness;
using BonitaSoftApiBusiness.Flow;
using BonitaSoftApiBusiness.Security;
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
    public class ProcessController : ControllerBase
    {
        /// <summary>
        /// Crea la instancia de un proceso
        /// </summary>
        /// <param name="process">Objeto con la información del proceso</param>
        /// <returns>Respuesta de la operación</returns>
        [Route("CreateProcess")]
        [HttpPost]
        public GenericResponse<string> CreateProcess(ProcesoDetalle process)
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
                    var proceso = this.FindProcess(process.NombreProceso, secure.Data);

                    if (proceso.Data != null)
                    {
                        var instanciaId = this.InstantiateProcess(process, proceso.Data, secure.Data);
                        
                        this.CloseSession(secure.Data);

                        return instanciaId;
                    }
                    else
                    {
                        this.CloseSession(secure.Data);

                        return new GenericResponse<string>() 
                        { 
                            Data = "No se encontro el id del proceso" 
                        };
                    }
                }
                else
                {
                    return Mapper<string>.MapGenericResponse(string.Empty, HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), "Wrong username or password", "Verify credentials");
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse(string.Empty, HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError.ToString(), $"Error en la instanciación del proceso.", $"{ ex.Message }");
            }
        }

        [Route("ReadCase")]
        [HttpGet]
        public GenericResponse<string> ReadCase(string idCaso)
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
                    var estado = this.ReadCase(idCaso, secure.Data);

                    this.CloseSession(secure.Data);

                    return estado;
                }
                else
                {
                    return Mapper<string>.MapGenericResponse(string.Empty, HttpStatusCode.Unauthorized, HttpStatusCode.Unauthorized.ToString(), "Wrong username or password", "Verify credentials");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Auxiliares
        /// <summary>
        /// Autentica al usuario
        /// </summary>
        /// <param name="requestAuthorization">Solicitud de autenticación</param>
        /// <returns>Respuesta solución</returns>
        private GenericResponse<SecurityContext> Authorize(RequestAuthorization requestAuthorization)
        {
            Authorizator authorizator = new Authorizator();

            return authorizator.Authenticate(requestAuthorization);
        }

        /// <summary>
        /// Busca un proceso por su nombre
        /// </summary>
        /// <param name="nombreProceso">Nombre del proceso</param>
        /// <param name="security">Contexto de seguridad</param>
        /// <returns>Respuesta del proceso</returns>
        private GenericResponse<string> FindProcess(string nombreProceso, SecurityContext security)
        {
            ProcessManager processManager = new ProcessManager();
            return processManager.GetProcessIdByName(nombreProceso, security);
        }

        /// <summary>
        /// Instancia un proceso
        /// </summary>
        /// <param name="proceso">Objeto de la orden</param>
        /// <param name="idProceso">Id del proceso</param>
        /// <param name="security">Contexto de seguridad</param>
        /// <returns>Resultado de la operación</returns>
        private GenericResponse<string> InstantiateProcess(ProcesoDetalle proceso, string idProceso, SecurityContext security)
        {
            ProcessManager processManager = new ProcessManager();
            return processManager.InstantiateProcess(proceso, idProceso, security);
        }

        private GenericResponse<string> ReadCase(string idCaso, SecurityContext security)
        {
            ProcessManager processManager = new ProcessManager();
            return processManager.ReadCase(idCaso, security);
        }

        /// <summary>
        /// Cierra la session
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
