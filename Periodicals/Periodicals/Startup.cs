using Microsoft.Owin;
using Owin;
using Periodicals.DAL.Repository.Abstract;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(Periodicals.Startup))]
namespace Periodicals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var factory = DependencyResolver.Current.GetService(typeof(IRepositoryFactory)) as IRepositoryFactory;
            ConfigureAuth(app);
        }
    }
}
