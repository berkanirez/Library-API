using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LibraryAPI2.Models
{
	public class Book
	{
		public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [StringLength(13,MinimumLength = 10)]
		[Column(TypeName = "varchar(13)")]
		public string? ISBN { get; set; }

		[Required]
		[StringLength(2000,MinimumLength = 1)]
		public string Title { get; set; } = "";

		[Range(1,short.MaxValue)]
		public short PageCount { get; set; }

		[Range(-4000,2100)]
		public short PublishingYear { get; set; }

		[StringLength(5000)]
		public string? Description { get; set; }

		[Range(0,int.MaxValue)]
		public int PrintCount { get; set; }

		public bool Banned { get; set; }

		[Range(0,5)]
		public float Rating { get; set; }

		public int TotalVotes { get; set; }

		public int PublisherId { get; set; }



		[StringLength(6, MinimumLength = 3)]
		[Column(TypeName = "varchar(6)")]
		public string LocationsShelf { get; set; } = "";

		public List<AuthorBook>? AuthorBooks { get; set; }

		public List<BookLanguage>? BookLanguages { get; set; }

        public List<BookSubCategory>? BookSubCategories { get; set; }

		public List<BookCopy>? BookCopies { get; set; }

        public List<Voting>? Votes { get; set; }


        [ForeignKey(nameof(PublisherId))]
		public Publisher? Publisher { get; set; }

		[JsonIgnore]
		[ForeignKey(nameof(LocationsShelf))]
		public Location? Locations { get; set; }
	}
}

