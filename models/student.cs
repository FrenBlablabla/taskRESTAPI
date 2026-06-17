using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace najnovijipokusajREST.Models
{
    [Table("Student", Schema = "dbo")]
    public class Student
    {
        [Key]
        public string Jmbag { get; set; } = null!;

        public string? Oib { get; set; }

        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public DateTime? DatumRodenja { get; set; }

        public string? Email { get; set; }

        public DateTime? DatumUpisa { get; set; }

        public string? Status { get; set; }

        public int? StudijskiProgramId { get; set; }
    }
}