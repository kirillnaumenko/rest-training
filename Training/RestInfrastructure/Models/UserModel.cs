using System;
using System.Text.Json.Serialization;

namespace Training.RestInfrastructure.Models
{
	public class UserModel
	{
        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        [JsonPropertyName("zipCode")]
        public string ZipCode { get; set; }
    }
}

