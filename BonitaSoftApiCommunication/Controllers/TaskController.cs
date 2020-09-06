using BonitaSoftApiBusiness;
using BonitaSoftApiBusiness.Flow;
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
    public class TaskController : ControllerBase
    {
        [Route("TakeTask")]
        [HttpGet]
        public GenericResponse<string> TakeTask(string caseId, string userId)
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
                    var tarea = this.GetTaskIdByCaseId(caseId, secure.Data);

                    var asignacion = this.AssignTask(tarea.Data, userId, secure.Data);

                    this.CloseSession(secure.Data);

                    return tarea;
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

        [Route("GetTaskName")]
        [HttpGet]
        public GenericResponse<string> GetTaskName(string caseId)
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
                    var tarea = this.GetTaskNameByCaseId(caseId, secure.Data);

                    this.CloseSession(secure.Data);

                    return tarea;
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

        [Route("RunTask")]
        [HttpPost]
        public GenericResponse<string> RunTask(ProcesoDetalle processDetail)
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
                    var done = this.DoTask(processDetail, secure.Data);

                    this.CloseSession(secure.Data);

                    return done;
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

        [Route("RunVariableTask")]
        [HttpGet]
        public GenericResponse<string> RunVariableTask(string caseId, string docComplete, string username, string pws)
        {
            try
            {
                RequestAuthorization requestAuthorization = new RequestAuthorization()
                {
                    Password = pws,
                    UserName = username
                };

                var secure = this.Authorize(requestAuthorization);

                if (secure.Data.SessionId != null)
                {
                    var tarea = this.GetTaskIdByCaseId(caseId, secure.Data);

                    var idUsuario = this.GetUserIdByName(username, secure.Data);

                    var asignacion = this.AssignTask(tarea.Data, idUsuario.Data, secure.Data);

                    var done = this.DoVariableTask(tarea.Data, docComplete, secure.Data);

                    this.CloseSession(secure.Data);

                    return tarea;
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

        private GenericResponse<string> GetUserIdByName(string userName, SecurityContext security)
        {
            UsuarioManager usuarioManager = new UsuarioManager();
            return usuarioManager.GetUserIdByname(userName, security);
        }

        private GenericResponse<SecurityContext> Authorize(RequestAuthorization requestAuthorization)
        {
            Authorizator authorizator = new Authorizator();

            return authorizator.Authenticate(requestAuthorization);
        }

        private GenericResponse<string> GetTaskIdByCaseId(string idCaso, SecurityContext security)
        {
            TaskManager taskManager = new TaskManager();
            return taskManager.GetTaskIdByCaseId(idCaso, security);
        }

        private GenericResponse<string> AssignTask(string taskId, string userId, SecurityContext security)
        {
            TaskManager taskManager = new TaskManager();
            return taskManager.TakeTask(taskId, userId, security);
        }

        private GenericResponse<string> DoTask(ProcesoDetalle procesoDetalle, SecurityContext security)
        {
            TaskManager taskManager = new TaskManager();
            return taskManager.RunTask(procesoDetalle, security);
        }

        private GenericResponse<string> DoVariableTask(string idTarea, string doccompleta, SecurityContext security)
        {
            TaskManager taskManager = new TaskManager();
            return taskManager.RunVariableTask(idTarea, doccompleta, security);
        }

        private GenericResponse<string> GetTaskNameByCaseId(string idCaso, SecurityContext security)
        {
            TaskManager taskManager = new TaskManager();
            return taskManager.GetTaskNameByCaseId(idCaso, security);
        }

        private void CloseSession(SecurityContext security)
        {
            Authorizator authorizator = new Authorizator();

            authorizator.CloseSession(security);
        }
        #endregion
    }
}