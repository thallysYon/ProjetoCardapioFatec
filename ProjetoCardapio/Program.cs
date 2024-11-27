using FluentAssertions.Common;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoCardapio.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com a conexão ao SQL Server
builder.Services.AddDbContext<ProjetoCardapioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetoCardapioContext") ??
    throw new InvalidOperationException("Connection string 'ProjetoCardapioContext' not found.")));

// Adicionando serviços do controlador e views
builder.Services.AddControllersWithViews();

// Configurando o limite de tamanho do arquivo (caso necessário)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB, ajuste conforme necessário
});

var app = builder.Build();

// Configurando o pipeline de requisição
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Configurando o acesso aos arquivos estáticos na pasta wwwroot (onde as imagens serão armazenadas)
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mapeando a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pedidos}/{action=Index}/{id?}");

app.Run();
