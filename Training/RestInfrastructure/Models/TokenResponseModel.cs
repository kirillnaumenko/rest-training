﻿using System;
using System.Text.Json.Serialization;

namespace Training.RestInfrastructure.Models
{
	public class TokenResponseModel
	{
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}

