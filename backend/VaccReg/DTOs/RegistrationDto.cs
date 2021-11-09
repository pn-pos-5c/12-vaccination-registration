namespace VaccReg.DTOs
{
    public class RegistrationDto
    {
        public long SocialSecurityNumber { get; set; }
        public long PinCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
