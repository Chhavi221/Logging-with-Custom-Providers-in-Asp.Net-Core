using Logger.Shared.Logging.DbLoggerObjects;
using Logger.Shared.Logging.FileLoggerObjects;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddDbLogger(options =>
{
    builder.Configuration.GetSection("Logging")
    .GetSection("Database").GetSection("Options").Bind(options);
});
builder.Logging.AddFileLogger(options => {
    builder.Configuration.GetSection("Logging").GetSection("File").GetSection("Options").Bind(options);
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o =>
    {
        o.SwaggerEndpoint("/swagger/v1/swagger.json", "Custom logging provider");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
