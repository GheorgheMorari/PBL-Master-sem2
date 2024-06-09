using MasterBackEnd.Api.Settings;
using MasterBackEnd.Identity.Application.Commands.Register;
using MasterBackEnd.Identity.Infrastructure.Startup;
using MasterBackEnd.ObjectStorage.Application.Commands;
using Minio;
using MasterBackEnd.ObjectStorage.Infrastructure.Startup;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo { Title = "MasterBackEnd", Version = "v1" });

     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
     {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
     });

     c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UploadFileCommand).Assembly));

builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddObjectStorageModule(builder.Configuration);

var minioSettings = builder.Configuration.GetSection("MinioCredentials").Get<MinioSettings>();

builder.Services.AddSingleton<IMinioClient>(sp =>
{
     return new MinioClient()
                 .WithEndpoint(minioSettings.Url)
                 .WithCredentials(minioSettings.Username, minioSettings.Password)
                 .Build();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
     app.UseDeveloperExceptionPage();
     app.UseSwagger();
     app.UseSwaggerUI(c =>
     {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasterBackEnd v1");
     });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
