using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WhereIsMyBusAPI.Contracts;
using WhereIsMyBusAPI.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WhereIsMyBusAPI.Security.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using WhereIsMyBusAPI.Security;

namespace WhereIsMyBusAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtConfigurations = new JwtConfigurations();
            new ConfigureFromConfigurationOptions<JwtConfigurations>(_configuration.GetSection("Jwt")).Configure(jwtConfigurations);

            var apiConfigurations = new APIConfigurations();
            new ConfigureFromConfigurationOptions<APIConfigurations>(_configuration.GetSection("API")).Configure(apiConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfigurations.Issuer,
                        ValidAudience = jwtConfigurations.Audience,
                        IssuerSigningKey = JwtSecurityKey.Create(jwtConfigurations.Key)
                    };
                });

            services.AddMvc();
            services.AddCors();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Where's my bus API",
                    Description = "API wrapper for SPTrans API",
                    TermsOfService = "N/A",
                    Contact = new Contact
                    {
                        Name = "Fabio Gemignani",
                        Email = "",
                        Url = "https://github.com/fgemig"
                    }
                });
            });

            services.AddSingleton(jwtConfigurations);
            services.AddSingleton(apiConfigurations);

            services.AddScoped<IHttpRequest<HttpRequestHandler>, HttpRequestHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
        }
    }
}
