using Erp.Api.Configuration;
using Erp.Api.Constants;
using Erp.Api.Middlewares;
using Erp.Api.Utils;
using Erp.Core.Entities.Account;
using Erp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Erp");
builder.Services.AddDbContext<ErpContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ErpContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

builder.Services.AddCors();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromDays(60);
});

builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

var startupConfiguration =
    builder.Configuration.GetSection(ConfigurationConstants.STARTUP).Get<StartupConfiguration>();

if (startupConfiguration.MigrateDatabase)
{
    using var scope = ((IApplicationBuilder) app).ApplicationServices.CreateScope();
    var dbContext = scope.ServiceProvider.GetService<ErpContext>();
    dbContext?.Database.Migrate();
}

app.UseCors(options =>
{
    options
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(hostName => true);
});

app.UseMiddleware<JwtTokenMiddleware>();
app.MapControllers();

app.Run();