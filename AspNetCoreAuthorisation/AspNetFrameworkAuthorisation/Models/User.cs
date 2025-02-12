using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AspNetFrameworkAuthorisation.Models
{
	public class User
	{
		public int Id { get; set; } 
		public string Username { get; set; } 
		public string Password { get; set; } 

		public bool Manager { get; set; }

		public bool Admin { get; set; }
	}

	public class AppDbContext: DbContext {
		public DbSet<User> Users { get; set; }
	}
}