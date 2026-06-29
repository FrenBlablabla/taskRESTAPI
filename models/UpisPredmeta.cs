using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace najnovijipokusajREST.Models
{
    [Table("UpisPredmeta", Schema = "dbo")]
    public class UpisPredmeta
    {
        [Key]
        public int Id { get; set; }
        public string StudentJmbag { get; set; } = null!;
        public string PredmetSifra { get; set; } = null!;
        public int AkademskaGodinaId { get; set; }
        public string? Status { get; set; }
        public DateTime? DatumUpisa { get; set; }
        public int BrojUpisa { get; set; }
    }
}