using ImagineApps.Application.Features.Bank.Queries.GetBankById;
using ImagineApps.Application.Features.FileHandler.Commands.TXTFromPath;
using ImagineApps.Application.Features.FileHandler.Commands.TXTFromStream;
using ImagineApps.Application.Utilities;
using ImagineApps.Infrastructure.ExternalResources;
using ImagineApps.Infrastructure.ExternalResources.Contracts;
using ImagineApps.Infrastructure.UnitOfWork.Contracts;
using ImagineApps.Infrastructure.UnitOfWork.Dapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ITXT, TXT>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetBankByIdQueryHandler)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TXTRequestFromPathHandler)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(TXTFromStreamRequestHandler)));
builder.Services.AddAutoMapper(typeof(Mappings));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", builder =>
    {
        builder.WithOrigins("http://localhost:3000");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
