namespace MagicSDK.Ext.OAuth.Modules
{
    public class OpenIDConnectProfile
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string PreferredUsername { get; set; }
        public string Profile { get; set; }
        public string Picture { get; set; }
        public string Website { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string Zoneinfo { get; set; }
        public string Locale { get; set; }
        public int? UpdatedAt { get; set; }

        // OpenIDConnectEmail
        public string Email { get; set; }
        public bool? EmailVerified { get; set; }

        // OpenIDConnectPhone
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberVerified { get; set; }

        // OpenIDConnectAddress
        public OIDAddress Address { get; set; }

        public class OIDAddress
        {
            public string Formatted { get; set; }
            public string StreetAddress { get; set; }
            public string Locality { get; set; }
            public string Region { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
        }
    }
}