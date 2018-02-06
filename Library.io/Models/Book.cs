using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.io.Models
{
    public class Book
    {
		public int ID { get; set; }
		public string Title { get; set; }
		public string Authors { get; set; }
		public int Available { get; set; }
		public string ISBN { get; set; }
		public string Edition { get; set; }
        public ICollection<Rental> Rentals { get; set; }
	}
}
