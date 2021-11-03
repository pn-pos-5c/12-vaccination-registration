﻿#nullable disable

using System.Text.Json.Serialization;

namespace VaccRegDb
{
    public partial class Registration
    {
        public long Id { get; set; }
        [JsonPropertyName("ssn")]
        public long SocialSecurityNumber { get; set; }
        [JsonPropertyName("pin")]
        public long PinCode { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        public virtual Vaccination Vaccination { get; set; }
    }
}
