namespace MyApp.Comments.Host.Api;

public static class ApiApplicationBuilderExtensions
{
    public static WebApplication UseCommentsApi(this WebApplication app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApp.Comments API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseDeveloperExceptionPage();
        }

        var disableHttps = app.Configuration.GetValue<bool>("DISABLE_HTTPS_REDIRECT");
        if (!disableHttps)
            app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        return app;
    }
}
