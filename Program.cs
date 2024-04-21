using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetProject.API.Services;
using Project.API.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
IMapper mapper = autoMapper.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("http://example.com",
                                             "http://www.contoso.com")
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                      });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
