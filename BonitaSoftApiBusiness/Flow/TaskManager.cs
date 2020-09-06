using BonitaSoftApiEntties.Entities;
using BonitaSoftApiEntties.Response;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;

namespace BonitaSoftApiBusiness.Flow
{
    public class TaskManager
    {
        public string Ruta { get; set; }

        public TaskManager()
        {
            Ruta = "http://10.39.1.194:8080";
        }

        public GenericResponse<string> GetTaskIdByCaseId(string idCase, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/bpm/humanTask?f=caseId=", idCase));
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                IRestResponse response = client.Execute(request);

                var content = response.Content.Length > 0 ? response.Content.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }) : string.Empty;

                if (!string.IsNullOrEmpty(content))
                {
                    return Mapper<string>.MapGenericResponse(JObject.Parse(content).GetValue("id").ToString(), HttpStatusCode.OK, HttpStatusCode.OK.ToString(), string.Empty, string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), "Id de tarea no existe", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        public GenericResponse<string> GetTaskNameByCaseId(string caseId, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/bpm/humanTask?f=caseId=", caseId));
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                IRestResponse response = client.Execute(request);

                var content = response.Content.Length > 0 ? response.Content.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }) : string.Empty;

                if (!string.IsNullOrEmpty(content))
                {
                    return Mapper<string>.MapGenericResponse(JObject.Parse(content).GetValue("name").ToString(), HttpStatusCode.OK, HttpStatusCode.OK.ToString(), string.Empty, string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), "No se obtiene ningun resultado", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        public GenericResponse<string> TakeTask(string taskId, string userId, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/bpm/humanTask/", taskId));
                var request = new RestRequest(Method.PUT);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddParameter("undefined", string.Concat("{ \r\n  \"assigned_id\" : \"", userId, "\"\r\n}"), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Mapper<string>.MapGenericResponse(string.Concat("Tarea ", taskId, " Asignada al id de usuario ", userId), HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "La tarea no se pudo asignar", string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "La tarea no se pudo asignar", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        public GenericResponse<string> RunTask(ProcesoDetalle processDetail, SecurityContext security)
        {
            try
            {
                var url = string.Concat(Ruta, "/bonita/portal/resource/taskInstance/",
                    processDetail.NombreProceso,
                    "/",
                    processDetail.Version,
                    "/",
                    processDetail.NombreTarea,
                    "/API/bpm/userTask/",
                    processDetail.IdTarea,
                    "/execution");
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return Mapper<string>.MapGenericResponse(processDetail.IdTarea, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "Tarea ejecutada correctamente", string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "No se pudo ejecutar la tarea", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        public GenericResponse<string> RunVariableTask(string taskId, string docComplete, SecurityContext security)
        {
            try
            {
                //var docu = new Documentos()
                //{
                //    documentacionCompletaInput = new DocumentacionCompletaInput()
                //    {
                //        docCompleta = docComplete
                //    }
                //};
                /*
                http://10.39.1.194:8080/bonita/portal/resource/taskInstance/Afiliaciones%20Skandia%20To%20Be/2.1/UT12001-(RPA)%20Verificar%20que%20los%20documentos%20esten%20diligenciados/API/bpm/userTask/20020/execution
                */
                var url = string.Concat(Ruta, "/bonita/portal/resource/taskInstance/Afiliaciones%20Skandia%20To%20Be/2.1/UT12001-(RPA)%20Verificar%20que%20los%20documentos%20esten%20diligenciados/API/bpm/userTask/", taskId, "/execution");
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(new object());
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Mapper<string>.MapGenericResponse(taskId, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "Tarea ejecutada de forma correcta", string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse(taskId, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "No se pudo ejecutar la tarea", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("-1", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }
    }
}
