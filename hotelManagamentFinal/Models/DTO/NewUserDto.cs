namespace hotelManagamentFinal.Models.DTO
{
    public class NewUserDto
    {
        public string emer { get; set; }
        public string mbiemer { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int RoleId { get; set; }
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string emer { get; set; }
        public string mbiemer { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int nrRezervimesh { get; set; }
        public int RoleId { get; set; }
        public byte? Invalidated { get; set; }
    }
}

