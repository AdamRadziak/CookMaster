namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateUserDTO
    {
        public string? EmailHash { get; set; }

        public string? PasswordHash { get; set; }

        public bool IsPassswordUpdate { get; set; } = true;

        public bool IsEmailUpdate { get; set; } = true;

    }
}
