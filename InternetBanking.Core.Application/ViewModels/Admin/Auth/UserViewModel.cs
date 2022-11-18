

namespace InternetBanking.Core.Application.ViewModels.Admin.Auth
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
