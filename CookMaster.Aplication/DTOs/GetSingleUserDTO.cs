namespace CookMaster.Aplication.DTOs
{
    public class GetSingleUserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdMenu { get; set; }

    }
}
