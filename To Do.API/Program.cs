using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using To_Do.API.Contexts;
using To_Do.API.Entities;
using To_Do.API.Helpers;
using To_Do.API.Services;
using To_Do.Services;

namespace To_Do.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            #region services
            /*Auto mapper*/
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            /*DbContexts*/
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                var connectionString = Environment.GetEnvironmentVariable(Constants.CONNECTION_STRING);
                option.UseSqlServer(connectionString);
            });

            /*Identity*/
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var signingKey = Environment.GetEnvironmentVariable(Constants.JWT_SIGNINGKEY);
                    var keyBytes = Encoding.UTF8.GetBytes(signingKey!);
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
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            /*Controllers*/
            builder.Services.AddControllers();
            builder.Services.AddUnitOfWork<ApplicationDbContext>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient<IUserProvider, UserProvider>();
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