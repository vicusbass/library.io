using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.io.Models
{
    public class Rental
    {
        public int ID { get; set; }
        public DateTime Expiration { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public ApplicationUser User {get; set; }
        public string ApplicationUserId { get; set; }
    }
}
