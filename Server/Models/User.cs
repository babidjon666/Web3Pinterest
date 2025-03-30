using System.Security.Cryptography;
using System.Text;

namespace Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string UserName { get; set; } = String.Empty;

        public User()
        {
            HashPassword();
        }

        private void HashPassword()
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));
                var builder = new StringBuilder();

                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                Password = builder.ToString();
            }
        }

        public async Task<bool> CheckEligble(Task<bool> email, Task<bool> userName)
        {
            bool isEmailTaken = await email;
            bool isUserNameTaken = await userName;

            if (isEmailTaken || isUserNameTaken)
            {
                return false;
            }
            return true;
        }
    }
}