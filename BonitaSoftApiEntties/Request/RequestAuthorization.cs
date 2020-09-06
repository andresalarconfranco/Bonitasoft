namespace BonitaSoftApiEntties.Request
{
    public class RequestAuthorization : GenericUserRequest
    {
        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string Password { get; set; }
    }
}
