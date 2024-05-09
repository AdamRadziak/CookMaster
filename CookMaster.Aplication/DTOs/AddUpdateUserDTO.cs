namespace CookMaster.Aplication.DTOs
{
    public class AddUpdateUserDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; } = null;

        public bool IsPassswordUpdate { get; set; } = true;

        public bool IsEmailUpdate { get; set; } = true;

    }
}
