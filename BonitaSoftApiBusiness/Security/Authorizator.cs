using BonitaSoftApiEntties.Entities;
using BonitaSoftApiEntties.Request;
using BonitaSoftApiEntties.Response;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace BonitaSoftApiBusiness.Security
{
    public class Authorizator
    {
        public string Ruta { get; set; }

        public Authorizator()
        {
            Ruta = "http://10.39.1.194:8080"; 
        }

        /// <summary>
        /// Inicia session
        /// </summary>
        /// <param name="requestAuthorization">Solicitud de la autorizacion</param>
        /// <returns>Contexto de seguridad</returns>
        public GenericResponse<SecurityContext> Authenticate(RequestAuthorization requestAuthorization)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/loginservice"));
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("undefined", string.Concat("username=", requestAuthorization.UserName, "&password=", requestAuthorization.Password, "&redirect=false"), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK && response.Cookies != null)
                {
                    SecurityContext security = new SecurityContext
                    {
                        XapiBonitaToken = response.Cookies.ToList().FirstOrDefault(x => x.Name == "X-Bonita-API-Token").Value.ToString(),
                        SessionId = response.Cookies.ToList().FirstOrDefault(x => x.Name == "JSESSIONID").Value.ToString(),
                        Cookie = response.Headers.ToList().FirstOrDefault(x => x.Name == "Set-Cookie").Value.ToString(),
                        Tenant = response.Cookies.ToList().FirstOrDefault(x => x.Name == "bonita.tenant").Value.ToString()
                    };

                    return Mapper<SecurityContext>.MapGenericResponse(security, 
                                                                    response.StatusCode, 
                                                                    response.StatusDescription, 
                                                                    "Authorization OK", 
                                                                    string.Empty);
                }
                else
                {
                    return Mapper<SecurityContext>.MapGenericResponse(new SecurityContext(),
                                                                    response.StatusCode,
                                                                    response.StatusDescription,
                                                                    "Authorization OK",
                                                                    string.Empty);
                }
            }
            catch (Exception ex)
            {
                return Mapper<SecurityContext>.MapGenericResponse(new SecurityContext(),
                                                HttpStatusCode.OK,
                                                HttpStatusCode.OK.ToString(),
                                                $"Authenticate method error: { ex.Message }",
                                                string.Empty);
            }
        }

        /// <summary>
        /// Cierra session
        /// </summary>
        /// <param name="security">Contexto de seguridad</param>
        public GenericResponse<string> CloseSession(SecurityContext security)
        {
            try
            {
                var client = new RestClient(string.Concat(Ruta, "/bonita/logoutservice"));
                var request = new RestRequest(Method.GET);
                request.AddHeader("X-Bonita-API-Token", security.XapiBonitaToken);
                request.AddHeader("JSESSIONID", security.SessionId);
                request.AddHeader("bonita.tenant", security.Tenant);
                request.AddCookie("bonita.tenant", security.Tenant);
                request.AddCookie("JSESSIONID", security.SessionId);
                request.AddCookie("X-Bonita-API-Token", security.XapiBonitaToken);
                IRestResponse response = client.Execute(request);

                return Mapper<string>.MapGenericResponse(string.Empty,
                                HttpStatusCode.OK,
                                HttpStatusCode.OK.ToString(),
                                $"Session closed correctly",
                                string.Empty);
            }
            catch (Exception ex)
            {
                return Mapper<string>.MapGenericResponse(string.Empty,
                                                HttpStatusCode.OK,
                                                HttpStatusCode.OK.ToString(),
                                                $"CloseSession method error: { ex.Message }",
                                                string.Empty);
            }
        }
    }
}
