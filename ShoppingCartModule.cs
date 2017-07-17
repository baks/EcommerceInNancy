namespace EcommerceInNancy
{
    using System.Threading;
    using System.Threading.Tasks;
    using Nancy;
    using Nancy.ModelBinding;

    public class ShoppingCartModule : NancyModule
    {
        public ShoppingCartModule(
            IShoppingCartStore shoppingCartStore, 
            IProductCatalogClient productCatalogClient, 
            IEventStore eventStore)
        {
            async Task<IShoppingCart> addingItemsToShoppingCart(dynamic parameters, CancellationToken _)
            {
                var productCatalogIds = this.Bind<int[]>();
                var userId = (int)parameters.userid;

                var userShoppingCart = shoppingCartStore.CartForUserOf(userId);
                var shoppingCartItems =
                    await productCatalogClient
                    .ShoppingCartItemsOf(productCatalogIds)
                    .ConfigureAwait(false);

                userShoppingCart.AddItems(shoppingCartItems, eventStore);
                shoppingCartStore.Save(userShoppingCart);

                return userShoppingCart;
            }

            Get("/{userid:int}", parameters => 
            {
                var userId = (int)parameters.userid;
                return shoppingCartStore.CartForUserOf(userId);
            });

            Post("/{userid:int}/items", addingItemsToShoppingCart);
        }
    }
}
