using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class AuthorBook
	{
        
		public long AuthorsId { get; set; }

        
        public int BooksId { get; set; }

        [ForeignKey(nameof(AuthorsId))]
		public Author? Author { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}

