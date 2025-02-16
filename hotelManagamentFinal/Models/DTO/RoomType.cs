using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotelManagamentFinal.Models.DTO
{
    public class RoomType1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Emer { get; set; }
        public decimal Siperfaqe { get; set; }
        public string? Pershkrim { get; set; }
        public int Kapacitet { get; set; }
    }
}
