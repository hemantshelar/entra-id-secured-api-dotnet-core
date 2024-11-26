using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

namespace entra_id_secured_api_dotnet_core
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			var tenantId = builder.Configuration["AzureAd:TenantId"];
			var clientId = builder.Configuration["AzureAd:ClientId"];
			var clientSecret = builder.Configuration["AzureAd:ClientSecret"];
			#region Inject Microsoft Graph
			var clientSecretCredential = new ClientSecretCredential(
tenantId, clientId, clientSecret);
			var scopes = new[] { "https://graph.microsoft.com/.default" };
			var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
			builder.Services.AddSingleton<GraphServiceClient>(graphClient);
			#endregion

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


			app.MapControllers();

			app.Run();
		}
	}
}
