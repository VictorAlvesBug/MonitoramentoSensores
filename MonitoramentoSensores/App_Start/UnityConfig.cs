using MonitoramentoSensores.BLL;
using MonitoramentoSensores.BLL.Interfaces;
using MonitoramentoSensores.DAL;
using MonitoramentoSensores.DAL.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MonitoramentoSensores
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IAreaBLL, AreaBLL>();
            container.RegisterType<IAreaDAL, AreaDAL>();
            container.RegisterType<ILoginBLL, LoginBLL>();
            container.RegisterType<ILoginDAL, LoginDAL>();
            container.RegisterType<IPlantaBLL, PlantaBLL>();
            container.RegisterType<IPlantaDAL, PlantaDAL>();
            container.RegisterType<ISensorBLL, SensorBLL>();
            container.RegisterType<ISensorDAL, SensorDAL>();
            container.RegisterType<IEquipamentoBLL, EquipamentoBLL>();
            container.RegisterType<IEquipamentoDAL, EquipamentoDAL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}