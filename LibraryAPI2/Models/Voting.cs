using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class Voting
	{
		public int Id { get; set; }

		public int BookId { get; set; }

        [JsonIgnore]
		public string? MemberId { get; set; } = "";

		[Range(1,5)]
		public float Vote { get; set; }

        [ForeignKey(nameof(BookId))]
        [JsonIgnore]
        public Book? Book { get; set; }

        [ForeignKey(nameof(MemberId))]
        [JsonIgnore]
        public Member? Member { get; set; }

        public bool IsDeleted { get; set; }

    }
}

