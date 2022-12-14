using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll();

    Product GetProductById(int id);

    Product UpdateProduct(int id, ProductDto productDto);

    Product SaveProduct(ProductDto productDto);

    void DeleteProduct(int id);

    List<Product> GetProductsByIds(int[] ids);

    bool IsEmpty();

    IEnumerable<Product> GetProductsByUserId(int id);

    int GetCountByProductId(int id);

    OrderSummaryDto GetOrderSummaryByUserId(int id);
    
    IEnumerable<ProductSummaryDto> GetProductSummary();

    int GetTotalIncome();
}