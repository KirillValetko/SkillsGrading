using SkillsGrading.Web.Infrastructure.Configuration;
using SkillsGrading.Web.Infrastructure.Middleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureAwsSecretsManager();
builder.Services.InitDbContext(builder.Configuration, builder.Environment);
builder.Services.InitRepositories();
builder.Services.InitServices();
builder.Services.InitHelpers();
builder.Services.InitMapper();
builder.Services.InitSwagger();
builder.Services.InitModelValidation();
builder.Services.InitAwsLambda(builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseVerification();

app.MapControllers();

app.Run();
