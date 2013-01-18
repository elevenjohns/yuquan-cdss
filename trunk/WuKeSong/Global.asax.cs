using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using WuKeSong.Areas.Music.Data;
using WuKeSong.Areas.Music.Models;

namespace WuKeSong
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public partial class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // HandleErrorAttribute represents an attribute that is used to handle an exception that is thrown by an action method.
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            // .axd files don't exist physically. ASP.NET uses URLs with .axd extensions (ScriptResource.axd and WebResource.axd) internally, and they are handled by an HttpHandler.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico"); //http://stackoverflow.com/questions/2109841/im-getting-a-does-not-implement-icontroller-error-on-images-and-robots-txt-in

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, null, new string[] { "WuKeSong.Controllers" }
            );
        }

        public static void RegisterLog()
        {            
            Trace.Listeners.Add(new TextWriterTraceListener(ConfigurationManager.AppSettings["LogFilePathName"]));
            Trace.AutoFlush = true; // This saves calling "Flush()" for each Write
        }

        protected void Application_Start()
        {
            // Areas are functionally independent modules of your ASP.NET MVC application that mimic the folder structure and conventions of MVC.     
            AreaRegistration.RegisterAllAreas();

            RegisterLog();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
            InitUnityContainer();
            InitPolicyInjection();            

            // When you deploy an application to a production web server, you must remove code that seeds the database.
            System.Data.Entity.Database.SetInitializer<MusicDBContext>(new MusicDBContextInitializer());

            // // Installs the deep ModelBinder
            // ModelBinders.Binders.DefaultBinder = new DeepModelBinder();  
        }

        // partial method as a place holder. Only when compile with MvcApplication.cs will it be implemented.
        // Partial methods must satify: 
        // # Signatures in both parts of the partial type must match.
        // # The method must return void.
        // # No access modifiers or attributes are allowed. Partial methods are implicitly private.
        static partial void InitUnityContainer();
        static partial void InitPolicyInjection();
    }
}