using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAPI2.Models
{
	public class Location
	{
        [Key]
        [Required]
        [StringLength(6, MinimumLength = 3)]
        [Column(TypeName = "varchar(6)")]
        public string Shelf { get; set; } = "";

        public List<Book>? Books { get; set; }

        public bool IsDeleted { get; set; }
    }
}

