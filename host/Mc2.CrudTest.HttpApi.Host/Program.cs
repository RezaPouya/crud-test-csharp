using Mc2.CrudTest.HttpApi.Host.Configurations;

namespace Mc2.CrudTest.HttpApi.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureServices();

            builder.ConfigureMiddleware();
        }
    }
}