using Mc2.CrudTest.HttpApi.Host.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

builder.ConfigureMiddleware();