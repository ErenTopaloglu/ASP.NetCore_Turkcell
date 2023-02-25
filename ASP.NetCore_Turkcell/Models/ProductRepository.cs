namespace ASP.NetCore_Turkcell.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
        {
           new() { Id = 1, Name = "Kalem", Price = 15, Stock = 100 },
           new () { Id = 2, Name = "Defter", Price = 10, Stock = 49 },
           new () { Id = 3, Name = "Çanta", Price = 258, Stock = 30 }

        };
        public List<Product> GetAll() => _products;
        public void Add(Product newProduct) => _products.Add(newProduct);
        public void Remove(int id)
        {
            var hasproduct = _products.FirstOrDefault(x => x.Id == id);
            if (hasproduct == null)
            {
                throw new Exception($"Bu id({id})'ye sahip ürün bulunmamaktadır.");
            }
            _products.Remove(hasproduct);
        }
        public void Update(Product updateproduct)
        {
            var hasproduct = _products.FirstOrDefault(x => x.Id == updateproduct.Id);
            if (hasproduct == null)
            {
                throw new Exception($"Bu id({updateproduct.Id})'ye sahip ürün bulunmamaktadır.");
            }
            hasproduct.Name = updateproduct.Name;
            hasproduct.Price = updateproduct.Price;
            hasproduct.Stock = updateproduct.Stock;

            var index = _products.FindIndex(x => x.Id == updateproduct.Id);
            _products[index] = hasproduct;

        }
    }
}
