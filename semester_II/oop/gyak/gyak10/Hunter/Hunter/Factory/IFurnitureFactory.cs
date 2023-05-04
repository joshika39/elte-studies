namespace Hunter.Factory
{
    public interface IFurnitureFactory
    {
        IChair CreateChair();
        ICoffeeTable CreateCoffeeTable();
        ISofa CreateSofa();
    }
}
