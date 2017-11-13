using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using CustomerManagement.Models;
using Newtonsoft.Json;

namespace CustomerManagementWebAPI.Controllers
{

	[RoutePrefix("api/colors")]
	public class ColorController : ApiController
    {
	    [HttpGet]
	    [Route("")]
		public HttpResponseMessage Get()
        {
	        IEnumerable<string> colors = Enum.GetNames(typeof(Color));
			var response = new HttpResponseMessage(HttpStatusCode.OK);
	        response.Content = new StringContent(JsonConvert.SerializeObject(colors));
	        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
	        return response;

		}
		
    }
}
