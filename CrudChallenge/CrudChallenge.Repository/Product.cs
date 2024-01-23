using System.ComponentModel.DataAnnotations;

namespace CrudChallenge.Repository
{
    public class Product
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public long Stock { get; set; }
    }
}
