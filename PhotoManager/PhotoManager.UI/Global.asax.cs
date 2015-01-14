using System.ServiceModel;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Domain;
using PhotoManager.Service;
using PhotoManager.UI.App_Start;
using PhotoManager.UI.Filters;

namespace PhotoManager.UI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private ServiceHost _userServiceHost;
        private ServiceHost _albumServiceHost;
        private ServiceHost _photoServiceHost;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalFilters.Filters.Add(new HandleAllErrorAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var container = IocConfig.GetContainerInstance();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AutoMapperConfig.Create();

            _userServiceHost = new ServiceHost(typeof(UserService));
            _userServiceHost.Open();

            _albumServiceHost = new ServiceHost(typeof(AlbumService));
            _albumServiceHost.Open();

            _photoServiceHost = new ServiceHost(typeof(PhotoService));
            _photoServiceHost.Open();
        }
        
        protected void Application_End()
        {
            if (_userServiceHost != null)
                _userServiceHost.Close();
            if (_photoServiceHost != null)
                _photoServiceHost.Close();
            if (_albumServiceHost != null)
                _albumServiceHost.Close();
        }

    }
}
