using System.Text;
using ImageVault.UploadService.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.Extension;
using ImageVault.UploadService.RabbitMq;
using ImageVault.UploadService.RabbitMq.Consumers;
using ImageVault.UploadService.Repository;
using ImageVault.UploadService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();
builder.AddSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(); 
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseRabbitMqListener();
app.MapControllers();
app.Run();