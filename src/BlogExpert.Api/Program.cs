using BlogExpert.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddApiConfig()
    .AddSwaggerConfig()
    .AddDbContextConfig()
    .AddIdentityConfig()
    .AddDependencias()
    .AddAutoMapper();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
