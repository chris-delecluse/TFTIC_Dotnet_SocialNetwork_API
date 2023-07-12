using SocialNetwork.WebApi.DependencyInjections;
using SocialNetwork.WebApi.Infrastructures.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCustomServices(builder.Configuration);
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.MapHub<AuthHub>("/social-network-auth");
app.MapHub<ChatHub>("/social-network-chat");
app.MapHub<PostHub>("/social-network-post");
app.MapHub<CommentHub>("/social-network-comment");
app.MapHub<LikeHub>("/social-network-like");

app.Run();
