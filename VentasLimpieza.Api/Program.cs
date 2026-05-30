using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VentasLimpieza.Api.Filters;
using VentasLimpieza.Core.Interfaces;
using VentasLimpieza.Infrastructure.Data;
using VentasLimpieza.Infrastructure.Mapping;
using VentasLimpieza.Infrastructure.Repositories;
using VentasLimpieza.Services.Interfaces;
using VentasLimpieza.Services.Services;
using VentasLimpieza.Services.Validators;

namespace VentasLimpieza.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            // Configurar BD
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<VentasLimpiezaContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Registrar repositorios y servicios
            // builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>(); 

            //services
            builder.Services.AddTransient<IUsuarioService, UsuariosService>();
            builder.Services.AddTransient<IProductoService, ProductoService>();
            builder.Services.AddTransient<IDetallepedidoService, DetallepedidoService>();
            builder.Services.AddTransient<ILoteproductoService, LoteproductoService>();
            builder.Services.AddTransient<ICodigoseguridadService, CodigoseguridadService>();
            builder.Services.AddTransient<ISecurityService, SecurityService>();
            builder.Services.AddTransient<IPedidoService, PedidoService>();


            //aditamientos
            builder.Services.AddScoped(
                typeof(IBaseRepository<>), 
                typeof(BaseRepository<>));
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();

            // Configurar Newtonsoft.Json para manejar ciclos de referencia
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(VentasLimpiezaProfile).Assembly);

            // Validadores
            builder.Services.AddScoped<UsuarioDtoValidator>();
            builder.Services.AddScoped<ProductoPorLoteValidator>();
            builder.Services.AddScoped<LoteproductoDtoValidator>();

            //Configurar JWT
            builder.Services.AddAuthentication(options =>
            {
                //Esquema por defecto para autenticar (identificar quien es el usuario)
                //Se va usar JWT Bearer como estándar
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                //si alguien intenta acceder si entar autenticado, se tiene que bloquear
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    //Valida el Emisor (iss) > Verifica que el token haya
                    //sido emitido por un servidor de confianza, Evitar que alguien use
                    //Tokens creados por otros sistemas
                    ValidateIssuer = true,

                    //Verifica que el token este dirigido a una API en particular
                    //un servicio de frontend, un cliente
                    ValidateAudience = true,

                    //Comprueba que el token no haya expirado
                    ValidateLifetime = true,

                    //Verifica que el token no haya sido modificada
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],

                    //Clave simétrica, esta misma sirve para firmar y verificar
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(
                            builder.Configuration["Authentication:SecretKey"])
                    )
                };
            });


            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>(); 

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); 
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}