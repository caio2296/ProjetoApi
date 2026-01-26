using System.Diagnostics;

namespace Projeto.Middleware
{
    public class MemoryLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MemoryLoggingMiddleware> _logger;

        public MemoryLoggingMiddleware(
            RequestDelegate next,
            ILogger<MemoryLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();


            var before = GC.GetTotalMemory(false);

            await _next(context);

            stopwatch.Stop();
            var after = GC.GetTotalMemory(false);

            _logger.LogInformation(
                "Path: {Path} | Tempo: {Elapsed} ms | Memória: {MemoryMB} MB",
                context.Request.Path,
                stopwatch.ElapsedMilliseconds,
                (after - before) / 1024 / 1024
            );
        }
    }

}