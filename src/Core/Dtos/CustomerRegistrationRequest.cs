namespace Core.Dtos
{
    public class CustomerRegistrationRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public short CountryId { get; set; }
    }
}
