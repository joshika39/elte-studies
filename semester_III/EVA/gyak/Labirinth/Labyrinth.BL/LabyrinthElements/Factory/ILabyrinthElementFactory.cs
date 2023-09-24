namespace Labyrinth.BL.LabyrinthElements.Factory
{
    public interface ILabyrinthElementFactory
    {
        ILabyrinthElement2D CreateFloor(int top, int left);
        ILabyrinthElement2D CreateWall(int top, int left);
    }
}
