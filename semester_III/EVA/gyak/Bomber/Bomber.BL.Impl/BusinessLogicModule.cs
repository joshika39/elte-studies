using Bomber.BL.Impl.Repositories;
using Bomber.BL.Impl.Settings;
using Bomber.BL.Repositories;
using Bomber.BL.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.BL.Impl
{
    public class BusinessLogicModule
    {
        public static void LoadModules(IServiceCollection collection)
        {
            collection.AddSingleton<IRouter, Router>();
            collection.AddSingleton<IMapGeneratorSettings, MapGeneratorSettings>();
        }
    }
}
