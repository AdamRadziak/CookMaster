namespace CookMaster.Aplication.DTOs
{
    public class GetSingleUserDTO
    {
        public int Id { get; set; }
        public string? EmailHash { get; set; }

        public string? PasswordHash { get; set; }

    }
}
