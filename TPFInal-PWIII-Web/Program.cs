using Microsoft.EntityFrameworkCore;
using TPFInal_PWIII.Data.Data;       
using TPFInal_PWIII.Logica;        
using TPFInal_PWIII.Logica.Interfaces; 
using Nethereum.Web3;              
using Nethereum.Web3.Accounts;      

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registrar el DbContext (Conexión a PostgreSQL en Render)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AventuraBlockchainDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Registrar Nethereum (Conexión a la Blockchain)
string nodeUrl = builder.Configuration["BlockchainSettings:NodeUrl"];
string privateKey = builder.Configuration["BlockchainSettings:BackendPrivateKey"]; // Lee de secrets.json

Account backendAccount = new(privateKey);
builder.Services.AddSingleton<Account>(backendAccount);
Web3 web3Instance = new Web3(backendAccount, nodeUrl);
builder.Services.AddSingleton<IWeb3>(web3Instance);


builder.Services.AddSingleton<JuegoLogica>();
builder.Services.AddSingleton<HistoriaLogica>();
builder.Services.AddSingleton<UsuarioLogica>();

builder.Services.AddSingleton<ILogicaDeJuego,JuegoLogica>();
builder.Services.AddSingleton<ILogicaHistorial,HistoriaLogica>();
builder.Services.AddSingleton<ILogicaJugador,UsuarioLogica>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers(); 

app.Run();