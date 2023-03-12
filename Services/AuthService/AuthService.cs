using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using App.Models;
using App.Dto.Auth;
using App.DBMagnament;

namespace App.Services.AuthService
{
    public class AuthService : IAuthService
    {
        public async Task<User?> Validate(LoginDto loginDto)
        {
            XDocument? xmlParam = DBXmlMethods.GetXml(loginDto);
            DataSet dsResultado = await DBXmlMethods.EjecutaBase(
                "Auth",
                "VALIDATE",
                xmlParam?.ToString()
            );

            if (dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
            {
                var data = dsResultado.Tables[0].Rows[0];
                return new User
                {
                    id = Convert.ToInt32(data["id"]),
                    username = data["username"].ToString(),
                    password = data["password"].ToString(),
                };
            }

            return null;
        }
    }
}
