using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassListWPF.Models
{
    [Table("Forms")]
    public class Form
    {
        #region Public Method

        public override string ToString() => Name;

        #endregion

        #region Public Property

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        #endregion
    }
}