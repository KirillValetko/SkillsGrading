using SkillsGrading.Web.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.InitDbContext(builder.Configuration);
builder.Services.InitRepositories();
builder.Services.InitServices();
builder.Services.InitHelpers();
builder.Services.InitMapper();
builder.Services.InitJwt();
builder.Services.InitSwagger();
builder.Services.InitModelValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
