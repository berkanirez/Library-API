using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class BookLanguage
	{
        public string LanguagesCode { get; set; } = "";

        public int BooksId { get; set; }

        [ForeignKey(nameof(LanguagesCode))]
        public Language? Language { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }
    }
}

