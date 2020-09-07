using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using Unity;
using WooliesX.Connector;
using WooliesX.Interface;

namespace WooliesX.Dependancy
{

  

    public static  class UnityHelper


    {


        private static IUnityContainer _container;
        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();

                    ConfigureContainer(_container);
                }
                return _container;
            }
        }


        public static IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer();
            ConfigureContainer(container);

            return container;
        }

        private static void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<IJsonConnector,  JsonConnector>();

        }
    }
}
