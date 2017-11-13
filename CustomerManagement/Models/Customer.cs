using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CustomerManagement.Models
{

	class CustomDateTimeConverter : IsoDateTimeConverter
	{
		public CustomDateTimeConverter()
		{
			base.DateTimeFormat = "M/d/yyyy";
		}
	}

	public class Customer
	{
		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public Gender Gender { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public Color FavoriteColor { get; set; }

		[JsonConverter(typeof(CustomDateTimeConverter))]
		public DateTime DateOfBirth { get; set; }
		
	}
}