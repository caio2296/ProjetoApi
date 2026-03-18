using System.Diagnostics;

namespace Projeto.Middleware
{
    public class MemoryLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MemoryLoggingMiddleware> _logger;

        // ✅ criado uma única vez no ciclo de vida da aplicação
        private static readonly Process _process =
            Process.GetCurrentProcess();

        public MemoryLoggingMiddleware(
            RequestDelegate next,
            ILogger<MemoryLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var memoryBefore = _process.WorkingSet64;

            return InvokeInternal(context, stopwatch, memoryBefore);
        }

        private async Task InvokeInternal(
            HttpContext context,
            Stopwatch stopwatch,
            long memoryBefore)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex,
                    "❌ Erro em {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                throw;
            }
            finally
            {
                stopwatch.Stop();

                _process.Refresh(); // 🔥 reaproveita instância

                var memoryAfter = _process.WorkingSet64;
                var memoryDiffMB =
                    (memoryAfter - memoryBefore) / 1024 / 1024;

                var statusCode = context.Response.StatusCode;

                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    _logger.LogWarning(
                        "🐢 LENTO | {Method} {Path} | {StatusCode} | {Elapsed} ms | ΔMem: {MemoryMB} MB",
                        context.Request.Method,
                        context.Request.Path,
                        statusCode,
                        stopwatch.ElapsedMilliseconds,
                        memoryDiffMB);
                }
                else
                {
                    _logger.LogInformation(
                        "{Method} {Path} | {StatusCode} | {Elapsed} ms | ΔMem: {MemoryMB} MB",
                        context.Request.Method,
                        context.Request.Path,
                        statusCode,
                        stopwatch.ElapsedMilliseconds,
                        memoryDiffMB);
                }
            }
        }
    }
}