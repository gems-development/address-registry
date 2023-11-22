using WebApi.Helpers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddDataAccess();
builder.Services.AddSerilogServices();


//log.logger = new loggerconfiguration()
//    .enrich.fromlogcontext()
//    .writeto.console()
//    .writeto.file("/var/log/addressregistryservice.log")
//    .createlogger();

//builder.host.configurelogging(logging =>
//{
//    logging.addserilog();
//    logging.setminimumlevel(loglevel.information);
//})
//.useserilog();




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
