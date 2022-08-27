using Mc2.CrudTest.WebUi.Configurations;
using Mc2.CrudTest.WebUi.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();

builder.ConfigureServices();


builder.ConfigureMiddleware();