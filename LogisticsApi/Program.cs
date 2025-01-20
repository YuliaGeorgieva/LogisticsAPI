using LogisticsApi.AuthenticationService;
using LogisticsApi.Dtos;
using LogisticsApi.Helpers;
using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    //password setting
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 1;

    //user setting
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    //lockout setting
    //options.Lockout.AllowedForNewUsers = true;
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(12);
    //options.Lockout.MaxFailedAccessAttempts = 3;

    //signIn setting
    //options.SignIn.RequireConfirmedPhoneNumber = true;
    //options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = false;

    //stores setting
    options.Stores.ProtectPersonalData = false;
    options.Stores.MaxLengthForKeys = 1;

});
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LogoutPath = "/Account/Logout";
    options.ExpireTimeSpan = TimeSpan.FromDays(2);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Error/AccessDenied";
    //options.SlidingExpiration = true;
    //options.Cookie.HttpOnly = true;
    //options.Cookie.IsEssential = true;
    //cookie.ReturnUrlParameter = "";
});


string AllowSpecificOrigins = "adminOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins, builder => builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
    //options.AddPolicy("CorsPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICompnayBranchRepository, CompnayBranchRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IShipmentDetailRepository, ShipmentDetailRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value); ;

builder.Services.AddAuthentication(options =>
{
    //options.RequireAuthenticatedSignIn = true;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        //ValidIssuer = builder.Configuration["JwtAuth:Issuer"],

        ValidateAudience = false,
        // ValidAudience = builder.Configuration["JwtAuth:Issuer"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ValidateLifetime = true,
    };
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Tourist API", Version = "v1" });

    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //option.IncludeXmlComments(xmlPath);

    //option.SwaggerDoc("v1", new OpenApiInfo
    //{
    //    Title = "Employee API",
    //    Version = "v1",
    //    Description = "An API to perform Employee operations",
    //    TermsOfService = new Uri("https://example.com/terms"),
    //    Contact = new OpenApiContact
    //    {
    //        Name = "John Walkner",
    //        Email = "John.Walkner@gmail.com",
    //        Url = new Uri("https://twitter.com/jwalkner"),
    //    },
    //    License = new OpenApiLicense
    //    {
    //        Name = "Employee API LICX",
    //        Url = new Uri("https://example.com/license"),
    //    }
    //});


    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
             {
                   new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                     new string[] {}
             }
     });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tourist API v1");
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<JwtMiddleware>();
app.MapControllers().RequireCors(AllowSpecificOrigins);


var scope = app.Services.CreateScope();
CreateSuperAdmin csa = new CreateSuperAdmin();
await csa.CreateSuperAdminFunc(scope.ServiceProvider);

app.Run();
