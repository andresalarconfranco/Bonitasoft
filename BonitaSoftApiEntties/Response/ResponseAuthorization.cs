namespace BonitaSoftApiEntties.Response
{
    public class ResponseAuthorization<T>
    {
        /// <summary>
        /// Codigo Http
        /// </summary>
        public string HttpCode { get; set; }

        /// <summary>
        /// Mensaje HTTP
        /// </summary>
        public string HttpMessage { get; set; }

        /// <summary>
        ///Codigo del mensaje 
        /// </summary>
        public string CodeMessage { get; set; }

        /// <summary>
        /// Data a ser devuelta
        /// </summary>
        public T Data { get; set; }
    }
}
