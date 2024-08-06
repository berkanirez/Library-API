using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class BookCopy
	{
		[Key]
		public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsHarmed { get; set; }

        public bool IsBorrowed { get; set; }

        [Required]
        public int BookId { get; set; }

		[ForeignKey(nameof(BookId))]
		[JsonIgnore]
        public Book? Book { get; set; }

		

	}
}

