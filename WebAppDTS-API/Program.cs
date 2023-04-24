using Microsoft.EntityFrameworkCore;
using WebAppDTS_API.Contexts;
using WebAppDTS_API.Repository.Contracts;
using WebAppDTS_API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAppDTS_API.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure MyContrext to SqlServer
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));

// Configure CORS
builder.Services.AddCors(options =>
                        options.AddDefaultPolicy(policy => {
                            policy.AllowAnyOrigin();
                            policy.AllowAnyHeader();
                            policy.AllowAnyMethod();
                        }));

// Configure Dependency Injection for Repositories

builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IProfilingRepository, ProfilingRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });

//https://blog.joaograssi.com/posts/2021/asp-net-core-protecting-api-endpoints-with-dynamic-policies/
//https://stackoverflow.com/questions/13870833/can-an-action-authorize-everyone-except-a-given-user-role
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("Get", policy => policy.RequireAssertion(context =>
//      context.User.HasClaim(c => c.Type == "User" && c.Value == "Read")));

//    options.AddPolicy("Post", policy => policy.RequireAssertion(context =>
//        context.User.HasClaim(c => c.Type == "Admin" && c.Value == "InsertAsync")));

//    options.AddPolicy("Put", policy => policy.RequireAssertion(context =>
//        context.User.HasClaim(c => c.Type == "Admin" && c.Value == "UpdateAsync")));

//    options.AddPolicy("Delete", policy => policy.RequireAssertion(context =>
//        context.User.HasClaim(c => c.Type == "Admin" && c.Value == "Delete")));
//});

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
//    options.AddPolicy("ITOnly", policy => policy.RequireClaim("Permission", "IT"));
//    options.AddPolicy("AdminRole", policy => policy.RequireRole("AdminRole"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
