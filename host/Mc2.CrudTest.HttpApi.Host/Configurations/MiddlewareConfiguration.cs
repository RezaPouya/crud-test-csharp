namespace Mc2.CrudTest.HttpApi.Host.Configurations
{
    internal static class MiddlewareConfiguration
    {
        internal static void ConfigureMiddleware(this WebApplicationBuilder? builder)
        {
            if (builder is null)
                throw new NullReferenceException("WebApplicationBuilder is null");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}