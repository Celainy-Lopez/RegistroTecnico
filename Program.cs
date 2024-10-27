using Microsoft.EntityFrameworkCore;
using RegistroTecnico.Components;
using RegistroTecnico.DAL;
using RegistroTecnico.Services;


namespace RegistroTecnico;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var ConStr = builder.Configuration.GetConnectionString("SqlConStr");
        builder.Services.AddDbContextFactory<Context>(c => c.UseSqlServer(ConStr));
        //Inyeccion del servicio (service)

        builder.Services.AddScoped<TecnicoService>();
        builder.Services.AddScoped<TipoTecnicoService>();
        builder.Services.AddScoped<ClienteService>();
        builder.Services.AddScoped<TrabajoService>();
        builder.Services.AddScoped<PrioridadService>();
        builder.Services.AddScoped<ArticuloService>();
        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
