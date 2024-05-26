using FavouriteColour.Extensions;
using Repository.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigurePersonService();
builder.Services.ConfigurePersonRepositoryFactory();
builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Service.MappingProfile));

builder.Services.Configure<ResourceOptions>(builder.Configuration.GetSection("ResourceOptions"));
builder.Services.ConfigureSwagger();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.API.AssemblyReference).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();
 

app.UseAuthorization();

app.MapControllers();

app.Run();
