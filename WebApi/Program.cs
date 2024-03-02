using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureScopedServices();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCors(builder.Environment);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
		 c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyDevHub v1");
	});
       
}
 
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseCors("cors");
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await context.Database.MigrateAsync();
    await context.SaveChangesAsync();
    
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.Seed(context);
}

app.Run();