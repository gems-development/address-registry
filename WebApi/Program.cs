using Gems.ApplicationServices.DependencyInjection;
using MediatR;
using WebApi.Dto;
using WebApi.Helpers;
using WebApi.UseCases.GetAddressById;
using WebApi.UseCases.GetAddressByLocation;
using WebApi.UseCases.GetAddressByName;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddDataAccess(true);
builder.Services.AddSerilogServices();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationServicesServiceCollectionExtension).Assembly);
});

builder.Services.AddScoped<IRequestHandler<GetAddressByIdRequest, AddressDtoResponse>, GetAddressByIdRequestHandler>();
builder.Services.AddScoped<IRequestHandler<GetAddressByLocationRequest, AddressDtoResponse>, GetAddressByLocationRequestHandler>();
builder.Services.AddScoped<IRequestHandler<GetAddressByNameRequest, AddressDtoResponse>, GetAddressByNameRequestHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
