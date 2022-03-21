using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SpringBoard.Data;
using SpringBoard.Data.Infrastructure;
using SpringBoard.Domaine;
using SpringBoard.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For Entity Framework  
builder.Services.AddDbContext<DatabContext>();

// For Identity  
builder.Services.AddIdentity<Utilisateur, IdentityRole>()
    .AddEntityFrameworkStores<DatabContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});
// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});



builder.Services.AddScoped<IServiceUser, ServiceUser>();

await CreateRoles();

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


 async Task CreateRoles()
{
    var RoleManager = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
    var UserManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<Utilisateur>>();

    IdentityResult roleResult;


    var roleCheck = await RoleManager.RoleExistsAsync("RH");
    var roleCheck1 = await RoleManager.RoleExistsAsync("Commercial");
    var roleCheck2 = await RoleManager.RoleExistsAsync("Consultant");
    var roleCheck3 = await RoleManager.RoleExistsAsync("Administrateur");


    if (!roleCheck)
    {
        roleResult = await RoleManager.CreateAsync(new IdentityRole("RH"));

    }
    if (!roleCheck1)
    {
        roleResult = await RoleManager.CreateAsync(new IdentityRole("Commercial"));

    }
    if (!roleCheck2)
    {

        roleResult = await RoleManager.CreateAsync(new IdentityRole("Consultant"));
    }
    if (!roleCheck3)
    {
        var role = new IdentityRole();
        role.Name = "Administrateur";
        await RoleManager.CreateAsync(role);
    }

    var checkuser = await UserManager.FindByEmailAsync("admin@admin.com");
    if (checkuser==null)
    {
        var usr = new Administrateur
        {
            LastName = "admin",
            Firstname = "admin",
            UserName = "admin@admin.com",
            Email = "admin@admin.com"
        };
        var chkUser = await UserManager.CreateAsync(usr, "karim123");
        if (chkUser.Succeeded)
        {
            var result1 = await UserManager.AddToRoleAsync(usr, "Administrateur");
        }
    }



    
}