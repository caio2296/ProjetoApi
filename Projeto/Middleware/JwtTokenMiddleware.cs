using Microsoft.IdentityModel.Tokens;
using Projeto.Token;
using System.Data;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;

public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _chaveSecreta;
    private readonly string _connectionString;

    public JwtTokenMiddleware(RequestDelegate next, string chaveSecreta, string connectionString)
    {
        _next = next;
        _chaveSecreta = chaveSecreta;
        _connectionString = connectionString;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Ignora requisição raiz ou swagger
        if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path == "/" || context.Request.Path.StartsWithSegments("/api/CriarToken"))
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
        {
            try
            {
                // Validação do JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = JwtSecurityKey.Creater(_chaveSecreta);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key
                }, out SecurityToken validatedToken);

                // Pega o idUsuario do token
                var jwtToken = (JwtSecurityToken)validatedToken;
                var usuarioId = jwtToken.Claims.First(x => x.Type == "idUsuario").Value;

                // Verificação do token no banco
                if (!await TokenValidoNoBanco(usuarioId, token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token inválido ou expirado no banco.");
                    return;
                }
            }
            catch
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token inválido.");
                return;
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token não fornecido.");
            return;
        }

        await _next(context);
    }

    private async Task<bool> TokenValidoNoBanco(string usuarioId, string token)
    {
        const string nomeProcedure = "VerificarTokenUsuario"; // sua procedure

        using (var conn = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand(nomeProcedure, conn))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", usuarioId);
            cmd.Parameters.AddWithValue("@TokenJWT", token);

            await conn.OpenAsync();

            var result = await cmd.ExecuteScalarAsync(); // a procedure deve retornar 1 se válido, 0 se não
            return result != null && Convert.ToInt32(result) == 1;
        }
    }
}
