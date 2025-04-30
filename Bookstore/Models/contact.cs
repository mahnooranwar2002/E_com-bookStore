using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class contact
    {
        [Key]
        public int id { get; set; }
    public string name { get; set; }
        public string email { get; set; }
        public string msg { get; set; }
    }
}
