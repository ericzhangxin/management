using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CustomerManagement.Models;
using CustomerManagement.Sorting;

namespace CustomerManagement.Management
{
	public class CustomerManager
	{
		private List<Customer> _customers = new List<Customer>();


		public IEnumerable<Customer> Customers
		{
			get { return _customers; }
		}

		public void AddCustomer(Customer cust)
		{
			if (_customers.Any())
			{
				int maxCustId = _customers.Max(c => c.Id);
				cust.Id = maxCustId + 1;
			}
			else
				cust.Id = 1;

			_customers.Add(cust);
		}

		public void DeleteCustomer(int id)
		{
			if (_customers.Any(c => c.Id == id))
				_customers.Remove(_customers.First(c => c.Id == id));

		}

		public void UpdateCustomer(int id, Customer customer)
		{
			if (_customers.Any(c => c.Id == id))
			{
				var current = _customers.First(c => c.Id == id);
				current.FirstName = customer.FirstName;
				current.LastName = customer.LastName;
				current.DateOfBirth = customer.DateOfBirth;
				current.FavoriteColor = customer.FavoriteColor;
				current.Gender = customer.Gender;

			}
		}


		public Customer GetCustomer(int id)
		{
			if (_customers.Any(c => c.Id == id))
				return _customers.First(c => c.Id == id);
			return null;
		}

		public IEnumerable<Customer> SortBy(IEnumerable<Tuple<string, SortDirection>> sortings)
		{
			//Marking Customer as Serializable will cause the JSON can't convert json string to object.
			//IEnumerable<Customer> sortedCopy = ObjectCopier.Clone(_customers);

			IList<SortDescription> descriptions = new List<SortDescription>();

			foreach (Tuple<string, SortDirection> sorting in sortings)
			{
				descriptions.Add(new SortDescription(sorting.Item1, sorting.Item2 == SortDirection.Ascending? ListSortDirection.Ascending: ListSortDirection.Descending));
			}


			var result = _customers.BuildOrderBys(descriptions.ToArray());

			return result;
		}
	}
}