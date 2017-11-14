using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerManagement.DataDisplayer;
using CustomerManagement.Management;
using CustomerManagement.Models;
using CustomerManagement.Sorting;

namespace CustomerManagement
{
	class Program
	{
		static void Main(string[] args)
		{
			var manager = new CustomerManager();

			manager.AddCustomer(new Customer() { LastName = "Zhang", FirstName = "Jenny", DateOfBirth = DateTime.Parse("11/8/2000"), FavoriteColor = Color.Blue, Gender = Gender.Female });
			manager.AddCustomer(new Customer() { LastName = "Josh", FirstName = "Peter", DateOfBirth = DateTime.Parse("1/20/1978"), FavoriteColor = Color.Red, Gender = Gender.Male });
			manager.AddCustomer(new Customer() { LastName = "Chris", FirstName = "Steve", DateOfBirth = DateTime.Parse("6/2/1997"), FavoriteColor = Color.Yellow, Gender = Gender.Male });

			IList<IList<Tuple<string, SortDirection>>> sortings = new List<IList<Tuple<string, SortDirection>>>();

			Tuple<string, SortDirection> sortingByGender = new Tuple<string, SortDirection>("Gender", SortDirection.Ascending);
			Tuple<string, SortDirection> sortingByLastName = new Tuple<string, SortDirection>("LastName", SortDirection.Ascending);
			IList<Tuple<string, SortDirection>> sortByGenderThenByLastName = new List<Tuple<string, SortDirection>>();
			sortByGenderThenByLastName.Add(sortingByGender);
			sortByGenderThenByLastName.Add(sortingByLastName);
			sortings.Add(sortByGenderThenByLastName);

			Tuple<string, SortDirection> sortingByDateOfBirth = new Tuple<string, SortDirection>("DateOfBirth", SortDirection.Ascending);
			IList<Tuple<string, SortDirection>> sortByDateOfBirth = new List<Tuple<string, SortDirection>>();
			sortByDateOfBirth.Add(sortingByDateOfBirth);
			sortings.Add(sortByDateOfBirth);

			Tuple<string, SortDirection> singleSortingByLastName = new Tuple<string, SortDirection>("LastName", SortDirection.Descending);
			IList<Tuple<string, SortDirection>> sortByLastName = new List<Tuple<string, SortDirection>>();
			sortByLastName.Add(singleSortingByLastName);
			sortings.Add(sortByLastName);


			var displayer = new ConsoleDisplayer(manager, sortings.ToList());

			displayer.Display();

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
