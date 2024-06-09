using MasterBackEnd.Api.Settings;
using MasterBackEnd.Identity.Application.Commands.Register;
using MasterBackEnd.Identity.Infrastructure.Startup;
using MasterBackEnd.ObjectStorage.Application.Commands;
using Minio;
using MasterBackEnd.ObjectStorage.Infrastructure.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

// Configure the HTTP request pipeline.
// TODO: add back the IsDev check
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
