using System.Data;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialNetwork.Domain.Repositories.Auth;
using SocialNetwork.Domain.Repositories.Comment;
using SocialNetwork.Domain.Repositories.Friend;
using SocialNetwork.Domain.Repositories.Like;
using SocialNetwork.Domain.Repositories.Post;
using SocialNetwork.WebApi.Infrastructures.AppStates;
using SocialNetwork.WebApi.Infrastructures.JWT;
using SocialNetwork.WebApi.Infrastructures.Users;
using SocialNetwork.WebApi.SignalR.Interfaces;
using SocialNetwork.WebApi.SignalR.Services;

namespace SocialNetwork.WebApi.Infrastructures.DependencyInjections;

internal static class ServiceExtensions
{
    internal static IServiceCollection AddCustomServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddSingleton<IDbConnection>(_ => new SqlConnection(configuration.GetConnectionString("Default")));
        service.AddSingleton<IUserConnectionState, UserConnectionState>();
        // V2
        service.AddSingleton<IConnectedUserManager, ConnectedUserManager>();
        
        service.AddScoped<IAuthRepository, AuthRepository>();
        service.AddScoped<IFriendRepository, FriendRepository>();
        service.AddScoped<IPostRepository, PostRepository>();
        service.AddScoped<ICommentRepository, CommentRepository>();
        service.AddScoped<ILikeRepository, LikeRepository>();
        service.AddScoped<ITokenService, TokenService>();
        
        service.AddScoped<IAuthHubService, AuthHubService>();
        service.AddScoped<IPostHubService, PostHubService>();
        service.AddScoped<ICommentHubService, CommentHubService>();
        
        service.AddSignalR();
        service.AddMediatR(opt => opt.RegisterServicesFromAssembly(typeof(Program).Assembly));

        AppDomain.CurrentDomain.GetAssemblies()
            .ToList()
            .ForEach(a => a
                .GetTypes()
                .Where(type => type is { IsAbstract: false, IsInterface: false, IsGenericType: false } &&
                               type.GetInterfaces()
                                   .Any(t =>
                                       t.IsGenericType &&
                                       t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
                                   )
                )
                .ToList()
                .ForEach(type => service.AddScoped(type.GetInterfaces().First(), type))
            );
        
        service.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
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
