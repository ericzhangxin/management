using System;
using System.Linq;
using CustomerManagement.DataReader;
using CustomerManagement.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerManagementTests.DataReader
{
	[TestClass]
	public class FileReaderTests
	{
		private CustomerManager _manager;
		private FileReader _fileReader;
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
			_fileReader = new FileReader(_manager);
		}

		[TestCleanup]
		public void TestCleanup()
		{

		}

		#endregion

		[TestMethod]
		public void Verify_ParseLine_good_data_expect_success()
		{
			string line = "Zhang|Eric|Male|Red|12/3/1997";
			var result = _fileReader.ParseLine(line, '|');

			//Customer should not be null
			Assert.IsTrue(result.Item1 != null);

			//No error message is returned
			Assert.IsTrue(result.Item2 == null);

			Assert.IsTrue(result.Item1 != null);
			Assert.IsTrue(result.Item1.FirstName == "Eric");
			Assert.IsTrue(result.Item1.LastName == "Zhang");
			Assert.IsTrue(result.Item1.DateOfBirth == DateTime.Parse("12/03/1997"));
		}

		[TestMethod]
		public void Verify_ParseLine_bad_data_at_Gender_expect_null_Customer_error_message_returned()
		{
			string line = "Zhang|Eric|MaleX|Red|12/3/1997";
			var result = _fileReader.ParseLine(line, '|');

			//Customer should be null
			Assert.IsTrue(result.Item1 == null);

			//Error message is returned
			Assert.IsTrue(result.Item2 != null);

			Assert.IsTrue((result.Item2 == "Error on parsing Gender from MaleX"));


		}

		[TestMethod]
		public void Verify_ParseLine_bad_data_at_Color_expect_null_Customer_error_message_returned()
		{
			string line = "Zhang|Eric|Male|RedX|12/3/1997";
			var result = _fileReader.ParseLine(line, '|');

			//Customer should be null
			Assert.IsTrue(result.Item1 == null);

			//Error message is returned
			Assert.IsTrue(result.Item2 != null);

			Assert.IsTrue((result.Item2 == "Error on parsing FavoriteColor from RedX"));


		}

		[TestMethod]
		public void Verify_ReadFile_good_data_expect_success()
		{
			string filePath = Environment.CurrentDirectory + "\\TestSampleFiles\\TestFile_Good_Comma.txt";
			_fileReader.ReadFile(filePath).GetAwaiter().GetResult();
			Assert.IsTrue(_manager.Customers.Count() == 2);

		}

		[TestMethod]
		public void Verify_ReadFile_bad_data_expect_only_one_customer_parsed()
		{
			string filePath = Environment.CurrentDirectory + "\\TestSampleFiles\\TestFile_Bad_FavoriteColor_Pipe.txt";
			_fileReader.ReadFile(filePath).GetAwaiter().GetResult();
			Assert.IsTrue(_manager.Customers.Count() == 1);

		}

	}
}