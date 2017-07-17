using System.Threading.Tasks;

namespace EcommerceInNancy
{
    public interface IProductCatalogClient
    {
        Task<object> ShoppingCartItemsOf(int[] productCatalogIds);
    }
}