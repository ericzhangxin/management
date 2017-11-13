using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManagement.Management;
using CustomerManagement.Models;
using CustomerManagement.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerManagementTests.Management
{
	[TestClass]
	public class CustomerManagerTests
	{
		private CustomerManager _manager;

		#region Initialize

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{

		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_manager = new CustomerManager();
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		#endregion


		[TestMethod]
		public void Verify_Sortings_by_Gender_ASC_then_by_LastName_ASC_expect_success()
		{
			_manager.AddCustomer(new Customer() { LastName = "Zhang", FirstName = "Jenny", DateOfBirth = DateTime.Parse("11/8/2000"), FavoriteColor = Color.Blue, Gender = Gender.Female });
			_manager.AddCustomer(new Customer() { LastName = "Josh", FirstName = "Peter", DateOfBirth = DateTime.Parse("1/20/1978"), FavoriteColor = Color.Red, Gender = Gender.Male });
			_manager.AddCustomer(new Customer() { LastName = "Chris", FirstName = "Steve", DateOfBirth = DateTime.Parse("6/2/1997"), FavoriteColor = Color.Yellow, Gender = Gender.Male });

			IList<Tuple<string, SortDirection>> sortings = new List<Tuple<string, SortDirection>>();
			sortings.Add(new Tuple<string, SortDirection>("Gender", SortDirection.Ascending));
			sortings.Add(new Tuple<string, SortDirection>("LastName", SortDirection.Ascending));
			IEnumerable<Customer> sorted = _manager.SortBy(sortings);

			Assert.IsTrue(sorted.First().LastName == "Zhang");
			Assert.IsTrue(sorted.First().FirstName == "Jenny");

			Assert.IsTrue(sorted.ElementAt(1).LastName == "Chris");
			Assert.IsTrue(sorted.ElementAt(1).FirstName == "Steve");

			Assert.IsTrue(sorted.ElementAt(2).LastName == "Josh");
			Assert.IsTrue(sorted.ElementAt(2).FirstName == "Peter");
		}


		[TestMethod]
		public void Verify_Sortings_by_DateOfBirth_ASC_expect_success()
		{
			_manager.AddCustomer(new Customer() { LastName = "Zhang", FirstName = "Jenny", DateOfBirth = DateTime.Parse("11/8/2000"), FavoriteColor = Color.Blue, Gender = Gender.Female });
			_manager.AddCustomer(new Customer() { LastName = "Josh", FirstName = "Peter", DateOfBirth = DateTime.Parse("1/20/1978"), FavoriteColor = Color.Red, Gender = Gender.Male });
			_manager.AddCustomer(new Customer() { LastName = "Chris", FirstName = "Steve", DateOfBirth = DateTime.Parse("6/2/1997"), FavoriteColor = Color.Yellow, Gender = Gender.Male });

			IList<Tuple<string, SortDirection>> sortings = new List<Tuple<string, SortDirection>>();
			sortings.Add(new Tuple<string, SortDirection>("DateOfBirth", SortDirection.Ascending));
			IEnumerable<Customer> sorted = _manager.SortBy(sortings);

			Assert.IsTrue(sorted.First().LastName == "Josh");
			Assert.IsTrue(sorted.First().FirstName == "Peter");

			Assert.IsTrue(sorted.ElementAt(1).LastName == "Chris");
			Assert.IsTrue(sorted.ElementAt(1).FirstName == "Steve");

			Assert.IsTrue(sorted.ElementAt(2).LastName == "Zhang");
			Assert.IsTrue(sorted.ElementAt(2).FirstName == "Jenny");
		}


	}
}