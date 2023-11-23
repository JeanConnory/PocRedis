using PocRedis.Repositories;
using PocRedis.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cfgEndPoint = builder.Configuration["Redis:Endpoint"];
var cfgPassword = builder.Configuration["Redis:Password"];

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.InstanceName = "";
    opt.Configuration = builder.Configuration["Redis:Endpoint"];
    opt.ConfigurationOptions = new ConfigurationOptions { Password = cfgPassword };
    opt.ConfigurationOptions.EndPoints.Add(cfgEndPoint);
});

builder.Services.AddScoped<ICacheRepository, RedisRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
