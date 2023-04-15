using System;
using System.Text.Json.Serialization;

namespace Training.RestInfrastructure.Models
{
	public class UpdateUserModel
	{
        [JsonPropertyName("userNewValues")]
        public UserModel userNewValues { get; set; }

        [JsonPropertyName("userToChange")]
        public UserModel userToChange { get; set; }
    }
}

