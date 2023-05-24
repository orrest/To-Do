using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using To_Do.API.Contexts;
using To_Do.API.Entities;
using To_Do.Helpers;

namespace To_Do.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services
            /*Auto mapper*/
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            /*DbContexts*/
            builder.Services.AddDbContext<UserRoleDbContext>(option =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                option.UseSqlServer(connectionString);
            });

            /*Identity*/
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
                    var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
                    var secKey = new SymmetricSecurityKey(keyBytes);
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = secKey
                    };
                });
            builder.Services.AddDataProtection(); /*necessary to identity services*/
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            }).AddRoles<Role>()
                .AddEntityFrameworkStores<UserRoleDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>();


            /*Controllers*/
            builder.Services.AddControllers();
            #endregion

            #region swagger
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #endregion

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}