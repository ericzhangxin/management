using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CustomerManagement.Management;
using CustomerManagement.Models;

namespace CustomerManagementWebAPI
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			CustomerManager customerManager = new CustomerManager();
			customerManager.AddCustomer(new Customer()
			{
				LastName = "Zhang",
				FirstName = "Eric",
				Gender = Gender.Male,
				FavoriteColor = Color.Blue,
				DateOfBirth = DateTime.Parse("11/8/1987")
			});

			customerManager.AddCustomer(new Customer()
			{
				LastName = "Josh",
				FirstName = "Unum",
				Gender = Gender.Female,
				FavoriteColor = Color.Yellow,
				DateOfBirth = DateTime.Parse("2/26/1995")
			});

			customerManager.AddCustomer(new Customer()
			{
				LastName = "Wahlin",
				FirstName = "Dan",
				Gender = Gender.Male,
				FavoriteColor = Color.Red,
				DateOfBirth = DateTime.Parse("2/26/2000")
			});

			HttpContext.Current.Cache.Insert("CustomerManager", customerManager);
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			//HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
			if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
			{
				HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
				HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
				HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
				HttpContext.Current.Response.End();
			}
		}
	}
}
