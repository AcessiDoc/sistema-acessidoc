// Copyright (c) 2023-2024. Todos os direitos reservados.
//
// Este arquivo é parte do projeto AcessiDoc - Sistema de Conversão de Provas
// e Livros para estudantes com baixa-visão. O código-fonte contido neste arquivo
// destina-se apenas como um complemento à documentação e é fornecido
// "como está", sem garantia de qualquer tipo, expressa ou implícita.

using Microsoft.EntityFrameworkCore;
using sistema_acessidoc.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Conexão com o pacote Npgsql do Postgre
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
