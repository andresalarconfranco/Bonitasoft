using BonitaSoftApiEntties.Entities;
using BonitaSoftApiEntties.Response;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;

namespace BonitaSoftApiBusiness.User
{
    public class UsuarioManager
    {
        public string Ruta { get; set; }

        public UsuarioManager()
        {
            Ruta = "http://10.39.1.194:8080";
        }

        /// <summary>
        /// Obtiene el id de un usuario por su nombre
        /// </summary>
        /// <param name="userName">Nombre de usuario</param>
        /// <param name="security">Contexto de seguridad</param>
        /// <returns>Respuesta generica</returns>
        public GenericResponse<string> GetUserIdByname(string userName, SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/API/identity/user?f=userName=", userName));
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
                    return Mapper<string>.MapGenericResponse(string.Empty, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), "User not found", string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse(string.Empty, HttpStatusCode.OK, HttpStatusCode.OK.ToString(), $"Error GetUserIdByname { ex.Message }.", string.Empty);
            }
        }
    }
}
