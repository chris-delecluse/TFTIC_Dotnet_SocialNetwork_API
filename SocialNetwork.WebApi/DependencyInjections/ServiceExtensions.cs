using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialNetwork.Domain.Repositories;
using SocialNetwork.Domain.Services;
using SocialNetwork.WebApi.Infrastructures.Token;
using SocialNetwork.WebApi.SignalR.Services.Auth;

namespace SocialNetwork.WebApi.DependencyInjections;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddSingleton<IDbConnection>(_ => new SqlConnection(configuration.GetConnectionString("Default")));

        service.AddScoped<IAuthHubService, AuthHubService>();

        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IAuthRepository, AuthService>();
        service.AddScoped<IPostRepository, PostService>();
        service.AddScoped<ICommentRepository, CommentService>();
        service.AddScoped<ILikeRepository, LikeService>();
        service.AddScoped<IFriendRepository, FriendService>();

        service.AddSignalR();
        
        service.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:63343")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
                });
        });
        service.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(jwtOpt =>
                {
                    jwtOpt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                }
            );
        service.AddAuthorization();

        service.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "social network api", Version = "v1" });
                option.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Enter a valid json web token (JWT)",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    }
                );
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            }
        );

        return service;
    }
}
