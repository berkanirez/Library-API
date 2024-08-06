using System;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI2.Models
{
	public class Methods
	{
		public bool YearChecker(short? a)
		{
			if( a > DateTime.Today.Year)
			{
				return false;
			}
			return true;
		}
	}
}

