using System.Data;
using System.Xml.Linq;
using App.Dto.Auth;
using App.DBMagnament;

namespace App.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public async Task<UserDto?> Validate(AuthDto authDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(authDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "Auth",
                "VALIDATE",
                xmlParam?.ToString()
            );

            if (dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                var data = dsResultado.Tables[0].Rows[0];
                return new UserDto
                {
                    id = Convert.ToInt32(data["id"]),
                    username = data["username"].ToString(),
                };
            }

            return null;
        }

        public async Task<string> Signin(AuthDto authDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(authDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "Auth",
                "SIGNIN",
                xmlParam?.ToString()
            );

            return "ok";
        }
    }
}
