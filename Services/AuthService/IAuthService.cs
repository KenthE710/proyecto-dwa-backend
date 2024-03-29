using App.Dto.Auth;

namespace App.Services.AuthService
{
    public interface IAuthService
    {
        ///<summary>
        /// Valida un usuario usando sus credenciales.
        ///</summary>
        /// <param name="authDto">Dto con las credenciales para validar a un usuario.</param>
        /// <returns>
        /// Retorna al usuario validado.
        /// Si las credenciales son incorrectas retorna null
        /// </returns>
        public Task<UserDto?> Validate(AuthDto authDto);

        public Task<string> Signin(AuthDto authDto);
    }
}
