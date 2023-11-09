namespace System.Infrastructure.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string email, string userName)
        {

            Email = email;
            UserName = userName;
        }
        public Guid? Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }
    }
}
