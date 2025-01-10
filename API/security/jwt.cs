using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class jwt
{

    public static string GenerateToken(string security,string body,int expiracionEnMinutos ){


        //Valida credencial de token 

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return "token";

    }
    
}
