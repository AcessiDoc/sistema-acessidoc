// Copyright (c) 2023-2024. Todos os direitos reservados.
//
// Este arquivo � parte do projeto AcessiDoc - Sistema de Convers�o de Provas
// e Livros para estudantes com baixa-vis�o. O c�digo-fonte contido neste arquivo
// destina-se apenas como um complemento � documenta��o e � fornecido
// "como est�", sem garantia de qualquer tipo, expressa ou impl�cita.

using Microsoft.EntityFrameworkCore;
using sistema_acessidoc.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Conex�o com o pacote Npgsql do Postgre
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<AcessiDocContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

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
