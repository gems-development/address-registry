using Gems.ApplicationServices.DependencyInjection;
using MediatR;
using WebApi.Dto.Response;
using WebApi.Handlers;
using WebApi.Helpers;
using WebApi.MediatrRequests;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddDataAccess();
builder.Services.AddSerilogServices();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationServicesServiceCollectionExtension).Assembly);
});

builder.Services.AddScoped<IRequestHandler<GetAddressByIdRequest, AddressDtoResponse>, GetAddressByIdHandler>();
builder.Services.AddScoped<IRequestHandler<GetAddressByLocationRequest, AddressDtoResponse>, GetAddressByLocationHandler>();
builder.Services.AddScoped<IRequestHandler<GetAddressByNameRequest, AddressDtoResponse>, GetAddressByNameHandler>();

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
