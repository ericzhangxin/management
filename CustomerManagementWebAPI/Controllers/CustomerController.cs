using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using CustomerManagement.Management;
using CustomerManagement.Models;
using CustomerManagement.Sorting;
using Newtonsoft.Json;

namespace CustomerManagementWebAPI.Controllers
{
    [RoutePrefix("records")]
    public class CustomerController : ApiController
    {
        private CustomerManager _customerManager;

        public CustomerController()
        {
	        CustomerManager custManager = HttpContext.Current.Cache["CustomerManager"] as CustomerManager;

	        if (custManager != null)
		        _customerManager = custManager;
		}

	    [HttpGet]
	    [Route("")]
	    public HttpResponseMessage GetCustomers()
	    {
		    try
		    {
			    return ComposeCustomersResponseMessage(_customerManager.Customers);
		    }
		    catch
		    {
			    return new HttpResponseMessage(HttpStatusCode.BadRequest);
		    }
	    }

		[HttpGet]
		[Route("gender")]
	    public HttpResponseMessage GetCustomersSortedByGender()
	    {
		    try
		    {
			    Tuple<string, SortDirection> sorting = new Tuple<string, SortDirection>("Gender", SortDirection.Ascending);
			    IList<Tuple<string, SortDirection>> sortings = new List<Tuple<string, SortDirection>>();
			    sortings.Add(sorting);
				var sortedCustomers = _customerManager.SortBy(sortings);
			    return ComposeCustomersResponseMessage(sortedCustomers);
			}
		    catch
		    {
			    return new HttpResponseMessage(HttpStatusCode.BadRequest);
		    }
	    }

	    [HttpGet]
	    [Route("birthdate")]
	    public HttpResponseMessage GetCustomersSortedByDateOfBirth()
	    {
		    try
		    {
			    Tuple<string, SortDirection> sorting = new Tuple<string, SortDirection>("DateOfBirth", SortDirection.Ascending);
			    IList<Tuple<string, SortDirection>> sortings = new List<Tuple<string, SortDirection>>();
			    sortings.Add(sorting);
				var sortedCustomers = _customerManager.SortBy(sortings);
			    return ComposeCustomersResponseMessage(sortedCustomers);
			}
		    catch
		    {
			    return new HttpResponseMessage(HttpStatusCode.BadRequest);
		    }
	    }

	    [HttpGet]
	    [Route("name")]
	    public HttpResponseMessage GetCustomersSortedByLastName()
	    {
		    try
		    {
			    Tuple<string, SortDirection> sorting = new Tuple<string, SortDirection>("LastName", SortDirection.Ascending);
			    IList<Tuple<string, SortDirection>> sortings = new List<Tuple<string, SortDirection>>();
			    sortings.Add(sorting);
				var sortedCustomers = _customerManager.SortBy(sortings);
			    return ComposeCustomersResponseMessage(sortedCustomers);
			}
		    catch
		    {
			    return new HttpResponseMessage(HttpStatusCode.BadRequest);
		    }
	    }

		[HttpPost]
	    [Route("")]
	    public HttpResponseMessage AddCustomer( Customer newCustomer)
	    {
		    try
		    {
			    _customerManager.AddCustomer(newCustomer);
			    return ComposeSingleCustomerResponseMessage(newCustomer);
		    }
		    catch
			{
			    return new HttpResponseMessage(HttpStatusCode.BadRequest);
		    }
	    }

	    [HttpDelete]
	    [Route("{id:int}")]
	    public HttpResponseMessage Delete(int id)
	    {
		    _customerManager.DeleteCustomer(id);

			return ComposeCustomersResponseMessage(_customerManager.Customers);
		}


	    [HttpGet]
	    [Route("{id:int}")]
	    public HttpResponseMessage GetCustomer(int id)
	    {
		    var customer = _customerManager.GetCustomer(id);

		    if (customer == null)
		    {
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}

		    return ComposeSingleCustomerResponseMessage(customer);
	    }

		[HttpPut]
	    [Route("{id:int}")]
	    public HttpResponseMessage Update(int id, [FromBody] Customer customer)
	    {
		    if (customer == null)
		    {
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}

		    if (!ModelState.IsValid)
		    {
				return new HttpResponseMessage(HttpStatusCode.BadRequest);
			}

		    _customerManager.UpdateCustomer(id, customer);
			
		    return ComposeSingleCustomerResponseMessage(customer);
	    }


	    HttpResponseMessage ComposeCustomersResponseMessage(IEnumerable<Customer> customers)
	    {
			var response = new HttpResponseMessage(HttpStatusCode.OK);
		    response.Content = new StringContent(JsonConvert.SerializeObject(customers));
		    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
		    return response;
		}

		HttpResponseMessage ComposeSingleCustomerResponseMessage(Customer customer)
		{
			var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StringContent(JsonConvert.SerializeObject(customer));
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			return response;
		}
	}
}
