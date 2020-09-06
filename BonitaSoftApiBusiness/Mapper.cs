using BonitaSoftApiEntties.Response;
using System.Net;

namespace BonitaSoftApiBusiness
{
    public static class Mapper<T>
    {
        #region Public Methods
        /// <summary>
        /// Map generic response
        /// </summary>
        /// <param name="data">Data to content</param>
        /// <param name="httpStatusCode">Http status code</param>
        /// <param name="httpStatusDesciption">Http status description</param>
        /// <param name="customMessage">Custom message</param>
        /// <param name="messageDetail">Message detail</param>
        /// <returns>Generic response</returns>
        public static GenericResponse<T> MapGenericResponse(T data, HttpStatusCode httpStatusCode, string httpStatusDesciption, string customMessage, string messageDetail)
        {
            return new GenericResponse<T>()
            {
                PropHttpStatusCode = httpStatusCode,
                PropHttpMessage = httpStatusCode.ToString(),
                PropCustomMessage = customMessage,
                PropMessageDetail = messageDetail,
                Data = data,
            };
        }
        #endregion
    }
}
