namespace BonitaSoftApiEntties.Entities
{
    public class SecurityContext
    {
        /// <summary>
        /// Id de la sesion
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Token generado por BonitaSoft
        /// </summary>
        public string XapiBonitaToken { get; set; }

        /// <summary>
        /// Tenant
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// Cookie
        /// </summary>
        public string Cookie { get; set; }
    }
}
