using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace WooliesX.Dependancy
{
    public class UnityFactory
    {

        protected static IUnityContainer Container;

        public UnityFactory()
        {

            if (Container == null)
            {
                Container = UnityHelper.GetUnityContainer();
            }
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
