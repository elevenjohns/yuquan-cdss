﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using WuKeSong.Interfaces;
using System.Web.Routing;
using System.Reflection;

namespace WuKeSong.Factories
{
    public class UnityControllerFactory : IControllerFactory
    {
        private IUnityContainer _container;
        private IControllerFactory _factory;

        public UnityControllerFactory(IUnityContainer container)
            : this(container, new DefaultControllerFactory()) { }


        public UnityControllerFactory(IUnityContainer container, IControllerFactory factory)
        {
            _container = container;
            _factory = factory;
        }

        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            try
            {
                return _container.Resolve<IController>(controllerName);
            }
            catch (Exception)
            {
                return _factory.CreateController(requestContext, controllerName);
            }
        }

        public System.Web.SessionState.SessionStateBehavior GetControllerSessionBehavior(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return System.Web.SessionState.SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            _container.Teardown(controller);
        }
    }
}