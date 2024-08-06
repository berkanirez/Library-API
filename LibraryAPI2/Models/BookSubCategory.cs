using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class BookSubCategory
	{
        public short SubCategoriesId { get; set; }

        public int BooksId { get; set; }

        [ForeignKey(nameof(SubCategoriesId))]
        public SubCategory? SubCategory { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}

