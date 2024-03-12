using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Review_Film_Project.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Review_Film_Project.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<review_phim_prn221Context>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesMovieContext")
						 ?? throw new InvalidOperationException("Connection string 'RazorPagesMovieContext' not found.")));
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Thêm HttpContextAccessor vào container dịch vụ

builder.Services.AddOptions();
var mailsettings = builder.Configuration.GetSection("MailSettings");  // đọc config
builder.Services.Configure<MailSettings>(mailsettings);               // đăng ký để Inject

builder.Services.AddTransient<IEmailSender, SendMailService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.Run();