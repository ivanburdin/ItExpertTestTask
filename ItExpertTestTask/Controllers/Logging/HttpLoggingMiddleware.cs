using System.Text;
using ItExpertTestTask.Dal.Repositories;

namespace ItExpertTestTask.Controllers.Logging;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpLoggingMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private const int SecondsTimeout = 10;

    public HttpLoggingMiddleware(RequestDelegate next,
        ILogger<HttpLoggingMiddleware> logger,
        IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        await LogRequest(context);

        var originalResponseBody = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;
            await _next.Invoke(context);

            await LogResponse(context, responseBody, originalResponseBody);
        }
    }

    private async Task LogResponse(HttpContext context, MemoryStream responseBody, Stream originalResponseBody)
    {
        var responseContent = new StringBuilder();
        responseContent.AppendLine("=== Response Info ===");
        responseContent.AppendLine($"path = {context.Request.Path}");
        responseContent.AppendLine($"status = {context.Response.StatusCode}");
        responseContent.AppendLine("-- headers");
        responseContent.AppendLine("-- headers");
        foreach (var (headerKey, headerValue) in context.Response.Headers)
        {
            responseContent.AppendLine($"header = {headerKey}    value = {headerValue}");
        }

        responseContent.AppendLine("-- body");
        responseBody.Position = 0;
        var content = await new StreamReader(responseBody).ReadToEndAsync();
        responseContent.AppendLine($"body = {content}");
        responseBody.Position = 0;
        await responseBody.CopyToAsync(originalResponseBody);
        context.Response.Body = originalResponseBody;

        var logMessage = responseContent.ToString();

        _logger.LogInformation(logMessage);
        await LogToDb(logMessage);
    }

    private async Task LogRequest(HttpContext context)
    {
        var requestContent = new StringBuilder();

        requestContent.AppendLine("=== Request Info ===");
        requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
        requestContent.AppendLine($"path = {context.Request.Path}");

        requestContent.AppendLine("-- headers");
        foreach (var (headerKey, headerValue) in context.Request.Headers)
        {
            requestContent.AppendLine($"header = {headerKey}    value = {headerValue}");
        }

        requestContent.AppendLine("-- body");
        context.Request.EnableBuffering();
        var requestReader = new StreamReader(context.Request.Body);
        var content = await requestReader.ReadToEndAsync();
        requestContent.AppendLine($"body = {content}");
        context.Request.Body.Position = 0;

        var logMessage = requestContent.ToString();

        _logger.LogInformation(logMessage);
        await LogToDb(logMessage);
    }

    private async Task LogToDb(string message)
    {
        using var scope = _scopeFactory.CreateScope();
        var logsRepository = scope.ServiceProvider.GetRequiredService<LogsRepository>();
        var token = new CancellationTokenSource(TimeSpan.FromSeconds(SecondsTimeout)).Token;

        await logsRepository.WriteToDb(message, token);
    }
}