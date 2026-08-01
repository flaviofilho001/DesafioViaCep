using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ViaCep.Data;
using ViaCep.Middlewares;
using ViaCep.Repositories;
using ViaCep.Services;
using ViaCep.Services.Exportacao;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registros de Injeção de Dependência (DI)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEnderecoService, EnderecoService>();
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

// Padrão Strategy para Exportação de Endereços
builder.Services.AddScoped<IExportadorStrategy, CsvExportadorStrategy>();
builder.Services.AddScoped<IExportadorEnderecos, CsvExportadorStrategy>();
builder.Services.AddScoped<IExportadorContext, ExportadorContext>();

// Configuração de Autenticação por Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware de tratamento global de exceções
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ViaCep API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();
