using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using priority_green_wave_api.Model;
using priority_green_wave_api.Repository;
using priority_green_wave_api.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<APIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CatadioptricoRepository>();
builder.Services.AddScoped<LocalizacaoCatadioptricoRepository>();
builder.Services.AddScoped<LocalizacaoRepository>();
builder.Services.AddScoped<LocalizacaoSemaforoRepository>();
builder.Services.AddScoped<SemaforoRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<VeiculoRepository>();
builder.Services.AddScoped<VeiculoUsuarioRepository>();


var encryptedKey = Encoding.ASCII.GetBytes(JWTKey.key);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(auth =>
{
    auth.RequireHttpsMetadata = false;
    auth.SaveToken = true;
    auth.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(encryptedKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
