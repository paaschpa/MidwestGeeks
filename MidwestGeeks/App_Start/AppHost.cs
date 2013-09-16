using System;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Mvc;
using MidwestGeeks.ServiceInterface;
using MidwestGeeks.ServiceInterface.Validators;
using ServiceStack.Configuration;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Mvc;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MidwestGeeks.App_Start.AppHost), "Start")]

namespace MidwestGeeks.App_Start
{
	public class AppHost
		: AppHostBase
	{		
		public AppHost() //Tell ServiceStack the name and where to find your web services
			: base("MidwestGeeks", typeof(MeetingsService).Assembly) { }

		public override void Configure(Funq.Container container)
		{
			//Set JSON web services to return idiomatic JSON camelCase properties
			ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;

		    container.Register<ICacheClient>(new MemoryCacheClient());
		    container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["Db"].ToString(), SqlServerDialect.Provider));

            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(MeetingValidator).Assembly);
			
            //Set MVC to use the same Funq IOC as ServiceStack
			ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
		}

		public static void Start()
		{
			new AppHost().Init();
		}
	}
}