/// <summary>
/// Copyright (c) 2023-2024. Acessidoc - Todos os direitos reservados.
///
/// Este arquivo � parte do projeto AcessiDoc - Sistema de Convers�o de Provas
/// para estudantes com baixa-vis�o. O c�digo-fonte contido neste arquivo
/// destina-se apenas como um complemento � documenta��o e � fornecido
/// "como est�", sem garantia de qualquer tipo, expressa ou impl�cita.
/// </summary>

using Microsoft.EntityFrameworkCore;
using sistema_acessidoc.Context;
using sistema_acessidoc.Models.Arquivos.InfraestruturaArquivo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Conex�o com SQLite
builder.Services.AddDbContext<AcessiDocContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

// Adiciona a refer�ncia a sess�o respns�vel pela cria��o da pasta
// onde os arquivos vindo do cliente ser�o armazenados
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