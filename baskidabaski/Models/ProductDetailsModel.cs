using Entity;

namespace baskidabaski.Models
{
    public class ProductDetailsModel
    {

        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
