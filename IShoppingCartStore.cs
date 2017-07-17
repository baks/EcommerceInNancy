public interface IShoppingCartStore
{
    IShoppingCart CartForUserOf(int userId);
    void Save(object userShoppingCart);
}