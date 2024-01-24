namespace CrudChallenge.Model
{
    public class CreateProductRequest
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public long Stock { get; set; }
    }
}
