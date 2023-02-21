namespace Projeto_Web_Lh_Pets_versão_1;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Projeto Web - LH Pets versão 1");

        app.UseStaticFiles(); // Permite acesso ao conteúdo estático (pasta wwwroot)

        app.MapGet("/index", (HttpContext contexto) =>
        {
            contexto.Response.Redirect("index.html", false);
        }); // Mapeamento rota "/index"

        Banco dba = new Banco();
        app.MapGet("/listaClientes", (HttpContext contexto) =>
        {
            contexto.Response.WriteAsync(dba.GetListaString()); // Escreve na page que estiver abrindo, os dados que virão do BD
        });

        app.Run();
    }
}
