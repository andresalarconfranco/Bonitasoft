using BonitaSoftApiEntties.Entities;
using BonitaSoftApiEntties.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;

namespace BonitaSoftApiBusiness.Flow
{
    public class ProcessManager
    {
        public string Ruta { get; set; }
        public ProcessManager()
        {
            Ruta = "http://10.39.1.194:8080";
        }

        /// <summary>
        /// Busca un proceso por su nombre
        /// </summary>
        /// <param name="nombreProceso">Nombre del proceso</param>
        /// <param name="security">Contexto de seguridad</param>
        /// <returns>Respuesta del proceso</returns>
        public GenericResponse<string> GetProcessIdByName(string nombreProceso, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/bpm/process?f=name=", nombreProceso));
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
                    return Mapper<string>.MapGenericResponse(nombreProceso, HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), "Not found results", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("ERROR", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        /// <summary>
        /// Instancia un proceso
        /// </summary>
        /// <param name="proceso">Objeto recibido por el proceso</param>
        /// <param name="processId">Id del proceso</param>
        /// <param name="security">Contexto de seguridad</param>
        /// <returns>Respuesta del proceso</returns>
        public GenericResponse<string> InstantiateProcess(ProcesoDetalle proceso, string processId, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/bpm/process/",processId, "/instantiation"));
                var request = new RestRequest(Method.POST);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddJsonBody(proceso);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = JsonConvert.DeserializeObject<Case>(response.Content);

                    return Mapper<string>.MapGenericResponse(content.caseId, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), string.Empty, string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse(processId, HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), "Cant Instantiate Process", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("ERROR", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }

        public GenericResponse<string> ReadCase(string idCaso, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/bpm/case/", idCaso));
                var request = new RestRequest(Method.POST);
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
                    return Mapper<string>.MapGenericResponse(JObject.Parse(content).GetValue("state").ToString(), HttpStatusCode.OK, HttpStatusCode.OK.ToString(), string.Empty, string.Empty);
                }
                else
                {
                    return Mapper<string>.MapGenericResponse(idCaso, HttpStatusCode.NotFound, HttpStatusCode.NotFound.ToString(), "No se pudo leer el caso", string.Empty);
                }

            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse("ERROR", HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"ERROR: { ex.Message }", string.Empty);
            }
        }
    }
}
