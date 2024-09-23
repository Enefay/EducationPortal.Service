using EducationPortal.API.Validators.EducationValidators;
using EducationPortal.BusinessLayer.Abstract;
using EducationPortal.BusinessLayer.Concrete;
using EducationPortal.DataAccessLayer.Abstract;
using EducationPortal.DataAccessLayer.Concrete;
using EducationPortal.DataAccessLayer.EntityFramework;
using EducationPortal.EntityLayer.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);






builder.Services.AddDbContext<EducationPortalContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EducationPortalDbContext"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //automapper


builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<IEducationService, EducationManager>();
builder.Services.AddScoped<IEducationDal, EfEducationDal>();
builder.Services.AddScoped<IContentService, ContentManager>();
builder.Services.AddScoped<IContentDal, EfContentDal>();

builder.Services.AddScoped<IEducationUserService, EducationUserManager>();
builder.Services.AddScoped<IEducationUserDal,EfEducationUserDal>();



// Identity Services

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    // Þifre gereksinimlerini devre dýþý býrakma
    options.Password.RequireDigit = false; // Rakam gereksinimi devre dýþý
    options.Password.RequireLowercase = false; // Küçük harf gereksinimi devre dýþý
    options.Password.RequireUppercase = false; // Büyük harf gereksinimi devre dýþý
    options.Password.RequireNonAlphanumeric = false; // Alfanumerik olmayan karakter gereksinimi devre dýþý
    options.Password.RequiredLength = 1; // Minimum þifre uzunluðu
    options.Password.RequiredUniqueChars = 0; // Farklý karakter sayýsý gereksinimi devre dýþý
    options.User.RequireUniqueEmail = true; // Benzersiz email gereksinimi
})
.AddEntityFrameworkStores<EducationPortalContext>()
.AddDefaultTokenProviders();


// Authentication via JWT Bearer Token

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Jwt:Issuer"],
          ValidAudience = builder.Configuration["Jwt:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(key)
      };
  });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") 
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateEducationDtoValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateEducationDtoValidator>();
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("Teacher"));
    options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("Student"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed roles on startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    await SeedRoles(roleManager); // Rollerin eklenmesini saðlýyoruz
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();


// SeedRoles metodu
async Task SeedRoles(RoleManager<AppRole> roleManager)
{
    string[] roleNames = { "Admin", "Teacher", "Student" };

    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new AppRole { Name = roleName });
        }
    }
}