using System.Text;
using HealthChecks.UI.Client;
using logTesting.Configs;
using logTesting.Configs.Filters;
using logTesting.Data;
using logTesting.Repo;
using logTesting.Services.Health;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// var logger = new LoggerConfiguration()
//     .ReadFrom.Configuration(builder.Configuration)
//     //.WriteTo.File(path:"log-.txt", )
//     .Enrich.FromLogContext()
//     .CreateLogger();
// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);

builder.Services.AddControllers(options => {
    options.Filters.Add(new MyFilter()); //Global filter
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck<ApiHealthCheck>("Jokes Health Check", tags: new string[] { "Jokes api" });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLDbConnection")));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; //Fallback
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt => {
    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters //Verify token
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,  //for dev
        ValidateAudience = false, //for dev
        RequireExpirationTime = false, //for dev -- set to true in production after including refresh token
        ValidateLifetime = true
    };
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
builder.Services.AddScoped<IArticleRepo, ArticleRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();


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
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
