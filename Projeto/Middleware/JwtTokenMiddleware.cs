using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Projeto.Token;
using System.Data;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _chaveSecreta;
    private readonly string _connectionString;
    private readonly ILogger<JwtTokenMiddleware> _logger;

    public JwtTokenMiddleware(RequestDelegate next, string chaveSecreta, string connectionString, ILogger<JwtTokenMiddleware> logger)
    {
        _next = next;
        _chaveSecreta = chaveSecreta;
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Ignora requisição raiz ou swagger
        if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path == "/" || context.Request.Path.StartsWithSegments("/api/CriarToken"))
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "Início da request {Method} {Path}",
            context.Request.Method,
            context.Request.Path
        );


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

                    _logger.LogWarning(
                   "Token inválido no banco para o usuário {UsuarioId}",
                   usuarioId
               );

                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token inválido ou expirado no banco.");
                    return;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(
               ex,
               "Erro ao validar token na rota {Path}",
               context.Request.Path
           );

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token inválido.");
                return;
            }
            finally
            {
                stopwatch.Stop();

                _logger.LogInformation(
                    "Fim da request {Method} {Path} - Status {StatusCode} - Tempo {Elapsed}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds
                );
            }
        }
        else
        {
            _logger.LogWarning(
               "Requisição sem token na rota {Path}",
               context.Request.Path
           );

            stopwatch.Stop();

            _logger.LogInformation(
                "Fim da request {Method} {Path} - Status {StatusCode} - Tempo {Elapsed}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds
            );


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
