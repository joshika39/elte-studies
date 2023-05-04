using Hunter.Furniture;

namespace Hunter.Factory
{
    public class VFurnitureFactory : IFurnitureFactory
    {

        public IChair CreateChair()
        {
            // some calcualtions
            return new VChair();
        }
        public ICoffeeTable CreateCoffeeTable()
        {
            throw new NotImplementedException();
        }
        public ISofa CreateSofa()
        {
            throw new NotImplementedException();
        }
    }
}
