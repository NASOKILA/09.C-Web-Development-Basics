namespace SimpleMvs.Framework
{
    using System;
    using System.Reflection;
    using WebServer;

    public static class MvcEngine
    {
        public static void Run(WebServer server) {

            RegisterAssemblyName();

            RegisterControllersData();

            RegisterViewsData();

            RegisterModelsData();

            RegisterResourcesData();

            try
            {
                server.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private static void RegisterResourcesData()
        {
            MvcContext.Get.ResourcesFolder = "Resources";
        }


        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName =
                Assembly.GetEntryAssembly().GetName().Name;
        }

        private static void RegisterModelsData()
        {
            MvcContext.Get.ModelsFolder = "Models";
        }

        private static void RegisterViewsData()
        {
            MvcContext.Get.ViewsFolder = "Views";
        }

        private static void RegisterControllersData()
        {
            MvcContext.Get.ControllersFolder = "Controllers";            
            MvcContext.Get.ControllersSuffix = "Controller";
        }
    }
}
