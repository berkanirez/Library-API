using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class Department
	{
		[Key]
		public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
		[StringLength(255, MinimumLength = 2)]
		public string Name { get; set; } = "";

		[StringLength(5000)]
		public string? Description { get; set; }

        [Phone]
        [StringLength(15, MinimumLength = 7)]
        [Column(TypeName = "varchar(15)")]
        public string? Phone { get; set; }

        [EmailAddress]
        [StringLength(320, MinimumLength = 3)]
        [Column(TypeName = "varchar(320)")]
        public string? EMail { get; set; }

        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}

