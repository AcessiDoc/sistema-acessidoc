/// <summary>
/// Copyright (c) 2023-2024. Acessidoc - Todos os direitos reservados.
///
/// Este arquivo é parte do projeto AcessiDoc - Sistema de Conversão de Provas
/// para estudantes com baixa-visão. O código-fonte contido neste arquivo
/// destina-se apenas como um complemento à documentação e é fornecido
/// "como está", sem garantia de qualquer tipo, expressa ou implícita.
/// </summary>

using Microsoft.EntityFrameworkCore;
using sistema_acessidoc.Context;
using sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Conexão com SQLite
builder.Services.AddDbContext<AcessiDocContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

// Adiciona a referência a sessão respnsável pela criação da pasta
// onde os arquivos vindo do cliente serão armazenados
builder.Services.Configure<ConfigurationFiles>(builder.Configuration.GetSection("ConfigurationDocumentsFolder"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Login}/{id?}");
});

app.Run();