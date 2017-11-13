﻿using System;
using System.Collections.Generic;
using CustomerManagement.Management;
using CustomerManagement.Models;
using CustomerManagement.Sorting;

namespace CustomerManagement.DataDisplayer
{
	public class ConsoleDisplayer
	{
		private readonly IEnumerable<IEnumerable<Tuple<string, SortDirection>>> _sortings;
		private CustomerManager _manager;
		public ConsoleDisplayer(CustomerManager manager, IEnumerable<IEnumerable<Tuple<string, SortDirection>>> sortings)
		{
			_manager = manager;
			_sortings = sortings;
			
		}

		public void Display()
		{
			foreach (IEnumerable<Tuple<string, SortDirection>> sorting in _sortings)
			{
				IEnumerable<Customer> sorted = _manager.SortBy(sorting);

				DisplayCustomers(sorted);

				Console.WriteLine(Environment.NewLine);
			}
		}

		private void DisplayCustomers(IEnumerable<Customer> sorted)
		{
			foreach (Customer customer in sorted)
			{
				Console.Write(customer.LastName);
				Console.Write(" ");
				Console.Write(customer.FirstName);
				Console.Write(" ");
				Console.Write(customer.Gender);
				Console.Write(" ");
				Console.Write(customer.FavoriteColor);
				Console.Write(" ");
				Console.Write(customer.DateOfBirth.ToString("M/d/yyyy"));
				Console.Write("\r\n");
			}
		}
	}


}