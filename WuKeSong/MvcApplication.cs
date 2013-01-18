using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WuKeSong.Interfaces;
using Microsoft.Practices.Unity;
using YuQuan.Interfaces;
using System.Web.Mvc;
using WuKeSong.Factories;
using WuKeSong.Services;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using YuQuan.Models;

namespace WuKeSong
{
    /// <summary>
    /// partial class definition for Unity Container Support
    /// </summary>
    public partial class MvcApplication : IContainerAccessor
    {
        private static IUnityContainer _container;
        /// <summary>
        /// The Unity container for the current application
        /// </summary>
        public static IUnityContainer Container { get { return _container; } }
        IUnityContainer IContainerAccessor.Container { get { return _container; } }

        protected void Application_End(object sender, EventArgs e)
        {
            if (Container != null)
                Container.Dispose();
        }

        static partial void InitUnityContainer()
        {
            if (_container == null)
            {
                _container = new UnityContainer();
            }

            // Register the relevant types for the 
            // container here through classes or configuration
            _container.RegisterType<IController, WuKeSong.Controllers.HomeController>("Home");
            // _container.RegisterType<IController, WuKeSong.Areas.DecisionSupport.Controllers.HomeController>("DSHome");
            UnityControllerFactory factory = new UnityControllerFactory(_container);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        /// <summary>
        /// This function demostrates EntLib Policy Injection usage. No actual use.
        /// 关于使用EntLib的PolicyInjection的几点说明[重要总结]
        /// # 需要添加Unity.*.dll的引用
        /// # 添加Reference时，确保EnterpriseLibrary.*.dll和Unity.*.dll是来自同一份EntLib安装的文件，因为前者对后者有依赖，版本信息必须严格匹配        
        /// </summary>
        static partial void InitPolicyInjection()
        {
            // PolicyInjection: A static facade class that provides the main entry point into the Policy Injection Application Block. Methods on this class create intercepted objects, or wrap existing instances with interceptors. 
            // 
            // The interception handler is defined in Web.config
            var obj = new ClinicalPathwayInstance();
            IComparable proxy = PolicyInjection.Wrap<IComparable>(obj); //or PolicyInjection.Create<ClinicalPathwayInstance,IComparable>
            proxy.CompareTo(null);
        }

        static partial void InitLogger()
        {
            LogEntry logEntry = new LogEntry();
            logEntry.Title = "Intiate Logger";
            Logger.Write(logEntry);
        }
    }
}