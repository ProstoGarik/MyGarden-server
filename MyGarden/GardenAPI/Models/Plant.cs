using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenAPI.Models
{
    [Table("plant")]
    public class Plant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("plant_id")]
        public int Id { get; set; }

        [Column("plant_title")]
        public string Title { get; set; } = "Some plant";

        [Column("plant_description")]
        public string Description { get; set; } = "Some description";
    }
}
