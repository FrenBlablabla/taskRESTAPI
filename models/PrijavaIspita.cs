using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace najnovijipokusajREST.Models
{
    [Table("PrijavaIspita", Schema = "dbo")]
    public class PrijavaIspita
    {
        [Key]
        public int Id { get; set; }
        public int UpisPredmetaId { get; set; }
        public int IspitniRokId { get; set; }
        public DateTime? DatumPrijave { get; set; }
        public int RedniBrojIzlaska { get; set; } 
        public string? Status { get; set; }
    }
}