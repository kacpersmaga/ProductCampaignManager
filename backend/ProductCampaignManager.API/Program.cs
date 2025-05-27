using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductCampaignManager.Application.Commands;
using ProductCampaignManager.Application.Interfaces;
using ProductCampaignManager.Application.Validators;
using ProductCampaignManager.Infrastructure.Persistence;
using ProductCampaignManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateCampaignCommand>();
});

builder.Services.AddValidatorsFromAssemblyContaining<CampaignDtoValidator>();

builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    SeedData.Seed(db);
}

app.Run();