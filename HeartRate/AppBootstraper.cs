using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Castle.Windsor;
using Castle.MicroKernel.Registration;

namespace HeartRate
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IHeartRateListener>().ImplementedBy<HeartRateListener>());
      
            DisplayRootViewFor<HeartRateMonitorViewModel>();
        }
    }
}
