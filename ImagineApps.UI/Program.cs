using ImagineApps.Application.Features.Bank.Queries.GetBankById;
using ImagineApps.Application.Features.FileHandler;
using ImagineApps.Application.Utilities;
using ImagineApps.Infrastructure.UnitOfWork.Contracts;
using ImagineApps.Infrastructure.UnitOfWork.Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetBankByIdQueryHandler)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TXTRequestHandler)));
builder.Services.AddAutoMapper(typeof(Mappings));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
