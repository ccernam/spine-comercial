using Microsoft.IdentityModel.Tokens;
using Spine.Entities.Seg;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Spine.Librerias.General
{
    public static class Utilitarios
    {
        public static string GetRefreshToken()
        {
            var varrRandomNumber = new byte[32];
            using (var vobjGenerator = RandomNumberGenerator.Create())
            {
                vobjGenerator.GetBytes(varrRandomNumber);
                return Convert.ToBase64String(varrRandomNumber);
            }
        }
        public static string GetAccessToken(Sesion pobjSesion)
        {
            List<Claim> vlstClaims = new List<Claim>();
            vlstClaims.Add(new Claim("iSesionId", Convert.ToString(pobjSesion.iSesionId)));
            vlstClaims.Add(new Claim("iUsuarioId", Convert.ToString(pobjSesion.iUsuarioId)));
            vlstClaims.Add(new Claim("sUsuUsuario", pobjSesion.sUsuUsuario ?? string.Empty));
            vlstClaims.Add(new Claim("sUsuCorreo", pobjSesion.sUsuCorreo ?? string.Empty));
            vlstClaims.Add(new Claim("iPersonaId", Convert.ToString(pobjSesion.iPersonaId ?? 0)));
            vlstClaims.Add(new Claim("sPerNombre", pobjSesion.sPerNombre ?? string.Empty));
            vlstClaims.Add(new Claim("iSucursalId", Convert.ToString(pobjSesion.iSucursalId ?? 0)));
            vlstClaims.Add(new Claim("sSucNombre", pobjSesion.sSucNombre ?? string.Empty));
            vlstClaims.Add(new Claim("sSesRefreshToken", pobjSesion.sSesRefreshToken));
            vlstClaims.Add(new Claim("dSesExpiracion", pobjSesion.dSesExpiracion.ToString("yyyyMMdd hh:mm")));

            var vobjTokenSeguridad = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: vlstClaims,
               expires: pobjSesion.dSesExpiracion,
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(
                       // Encoding.UTF8.GetBytes(_objConfiguration.GetSection("JWT:Key").Value)
                       Encoding.UTF8.GetBytes("26f4dd71e8264f1aa8414884cc9d90ad")
                    ),
                   SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(vobjTokenSeguridad);
        }
        public static Sesion GetSesion(string psSesAccessToken, bool plValidarExpiracion)
        {
            ClaimsPrincipal vobjPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                psSesAccessToken,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("26f4dd71e8264f1aa8414884cc9d90ad")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = plValidarExpiracion,
                    ClockSkew = TimeSpan.Zero
                },
                out SecurityToken vobjTokenRetornado
            );

            JwtSecurityToken vsTokenValidado = (JwtSecurityToken)vobjTokenRetornado;

            if (vsTokenValidado == null || !vsTokenValidado.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                throw new Exception("'Token' no ha podido ser validado");

            Sesion vobjSesion = new Sesion();
            vobjSesion.iSesionId = Convert.ToInt32(vobjPrincipal.FindFirst("iSesionId").Value);
            vobjSesion.iUsuarioId = Convert.ToInt32(vobjPrincipal.FindFirst("iUsuarioId").Value);
            vobjSesion.sUsuUsuario = vobjPrincipal.FindFirst("sUsuUsuario").Value;
            vobjSesion.sUsuCorreo = vobjPrincipal.FindFirst("sUsuCorreo").Value;
            vobjSesion.iPersonaId = Convert.ToInt32(vobjPrincipal.FindFirst("iPersonaId").Value);
            vobjSesion.sPerNombre = vobjPrincipal.FindFirst("sPerNombre").Value;
            vobjSesion.iSucursalId = Convert.ToInt32(vobjPrincipal.FindFirst("iSucursalId").Value);
            vobjSesion.sSucNombre = vobjPrincipal.FindFirst("sSucNombre").Value;
            vobjSesion.sSesRefreshToken = vobjPrincipal.FindFirst("sSesRefreshToken").Value;
            vobjSesion.dSesExpiracion = DateTime.ParseExact(vobjPrincipal.FindFirst("dSesExpiracion").Value, "yyyyMMdd hh:mm", CultureInfo.InvariantCulture);

            return vobjSesion;
        }

        public static DateTime GetFechaSistema()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "SA Pacific Standard Time");
        }

        public static Exception GetValidacion(string psMensaje)
        {
            var vobjExcepcion = new Exception(psMensaje);
            vobjExcepcion.Data["lEsVal"] = true;
            return vobjExcepcion;
        }

        public static bool EsValidacion(Exception pobjExcepcion)
        {
            return pobjExcepcion.Data["lEsVal"] == null ? false : Convert.ToBoolean(pobjExcepcion.Data["lEsVal"]);
        }

        public static void VerificarCadenas<T>(T pobjValidar)
        {
            var varrProperties = typeof(T).GetProperties().Where(x => x.PropertyType == typeof(string)).ToArray();
            foreach (var vobjProperty in varrProperties)
            {
                var vobjValor = vobjProperty.GetValue(pobjValidar);
                if (vobjValor == null)
                    vobjProperty.SetValue(pobjValidar, string.Empty);
                else
                    vobjProperty.SetValue(pobjValidar, vobjValor.ToString().Trim());
            }
        }

        public static bool ValidarAlfabetico(string psCadena, string psSimbolos = "", bool plEspacios = true)
        {
            string vsExpresion = @"^[a-zA-ZáéíóíüÁÉÍÓÚÜÑñ";
            if (plEspacios == true)
                vsExpresion += "\\s";
            vsExpresion += "\\b";
            if (psSimbolos.IndexOf("-") != -1 && psSimbolos.Length > 0)
                psSimbolos = psSimbolos.Replace("-", "\\-");
            vsExpresion += psSimbolos + "]+$";
            return new Regex(vsExpresion).IsMatch(psCadena);
        }

        public static bool ValidarAlfanumerico(string psCadena, string psSimbolos = "", bool plEspacios = true)
        {
            string vsExpresion = @"^[a-zA-ZáéíóíüÁÉÍÓÚÜ0-9Ññ";
            if (plEspacios == true)
                vsExpresion += "\\s";
            vsExpresion += "\\b";
            if (psSimbolos.IndexOf("-") != -1 && psSimbolos.Length > 0)
                psSimbolos = psSimbolos.Replace("-", "\\-");
            vsExpresion += psSimbolos + "]+$";
            return new Regex(@vsExpresion).IsMatch(psCadena);
        }
    }
}
