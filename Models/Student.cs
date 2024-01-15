using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst_EF.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        [Column("StudentName",TypeName ="varchar(100)")]
        public string Name { get; set; }

        [Column("StudentGender", TypeName = "varchar(10)")]

        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        public int Standard { get; set; }    
    }
}
