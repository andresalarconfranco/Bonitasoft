using System.Net;

namespace BonitaSoftApiEntties.Response
{
    public class GenericResponse<T>
    {
        #region Properties
        /// <summary>
        /// Http Code
        /// </summary>
        public HttpStatusCode PropHttpStatusCode { get; set; }

        /// <summary>
        /// Http Message
        /// </summary>
        public string PropHttpMessage { get; set; }

        /// <summary>
        /// Custom message
        /// </summary>
        public string PropCustomMessage { get; set; }

        /// <summary>
        /// Message Detail
        /// </summary>
        public string PropMessageDetail { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; }
        #endregion
    }
}
