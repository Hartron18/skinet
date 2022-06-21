namespace SkinetApi.Extensions
{
    public static class SwaggerDocExtensions
    {
        public static IServiceCollection AddSwaggerDoc( this IServiceCollection Services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddSwaggerGen();


            return Services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
