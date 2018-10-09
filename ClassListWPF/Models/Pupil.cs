using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassListWPF.Models
{
    [Table("Pupils")]
    public class Pupil
    {
        #region Public Property

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(32)]
        public string Class { get; set; }

        [Required]
        [StringLength(64)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(64)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        #endregion
    }
}