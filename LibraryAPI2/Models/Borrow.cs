using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LibraryAPI2.Models
{
	public class Borrow
	{
		[Key]
		public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public int BookId { get; set; }

		public string MemberId { get; set; } = "";

		public string EmployeeId { get; set; } = "";

		public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

		public DateTime? ReceiveDate { get; set; }

		public float? PunishmentTaken { get; set; }

		public bool IsHarmed { get; set; }

		[ForeignKey(nameof(MemberId))]
        [JsonIgnore]
        public Member? Member { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [JsonIgnore]
        public Employee? Employee { get; set; }

        [ForeignKey(nameof(BookId))]
		[JsonIgnore]
		public BookCopy? BookCopy { get; set; }



	}
}

