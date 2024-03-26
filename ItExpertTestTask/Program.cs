using ItExpertTestTask.Bll;
using ItExpertTestTask.Controllers.Logging;
using ItExpertTestTask.Dal;
using ItExpertTestTask.Dal.Repositories;
using ItExpertTestTask.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(DataMappingProfile));

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddScoped<DataValuesService>();

builder.Services.AddScoped<DataRepository>();
builder.Services.AddScoped<LogsRepository>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HttpLoggingMiddleware>();

app.UseHttpsRedirection();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DataContext>();
    context.Database.Migrate();
}

app.Run();