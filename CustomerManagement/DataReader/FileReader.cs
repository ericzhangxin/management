using System;
using System.IO;
using System.Threading.Tasks;
using CustomerManagement.Management;
using CustomerManagement.Models;
using CustomerManagement.Models;
namespace CustomerManagement.DataReader
{
	public class FileReader
	{
		private CustomerManager _manager;

		public FileReader(CustomerManager manager)
		{
			_manager = manager;
		}


		public Tuple<Customer, string> ParseLine(string line, char delimiter)
		{

			string[] data = line.Split(delimiter);

			Customer cust = new Customer();

			cust.LastName = data[0];
			cust.FirstName = data[1];

			Gender gender;
			if (Enum.TryParse(data[2], out gender))
				cust.Gender = gender;
			else
				return new Tuple<Customer, string>(null, $"Error on parsing Gender from {data[2]}");

			Color favColor;
			if (Enum.TryParse(data[3], out favColor))
				cust.FavoriteColor = favColor;
			else
				return new Tuple<Customer, string>(null, $"Error on parsing FavoriteColor from {data[3]}");

			DateTime dob;
			if (DateTime.TryParse(data[4], out dob))
				cust.DateOfBirth = dob;
			else
				return new Tuple<Customer, string>(null, $"Error on parsing DateOfBirth from {data[4]}");


			return new Tuple<Customer, string>(cust, null);
		}

		public async Task ReadFile(string filepath)
		{
			string line;
			bool firstLine = true;
			char delimiter = ' ';
			using (StreamReader reader = File.OpenText(filepath))
			{
				while ((line = await reader.ReadLineAsync()) != null)
				{
					if (firstLine)
					{
						if (line.IndexOf('|') != -1)
							delimiter = '|';

						if (line.IndexOf(',') != -1)
							delimiter = ',';

						firstLine = false;

					}

					var result = ParseLine(line, delimiter);

					if (result.Item1 != null) //Parse the line correctly
					{
						_manager.AddCustomer(result.Item1);
					}
					else
					{
						Console.WriteLine(result.Item2);
					}
				}
			}
		}


	}
}