using AppointmentBooking.Shared;
using AppointmentBooking.Ui.Pages;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

AppConfig.ConnectionString = configuration.GetConnectionString("Default")!;
AppConfig.Username = configuration.GetSection("smtp")["Username"]!;
AppConfig.Password = configuration.GetSection("smtp")["Password"]!;

await new AppMenu().ShowAppMenuAsync();
