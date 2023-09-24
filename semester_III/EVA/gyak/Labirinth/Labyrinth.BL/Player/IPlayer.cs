namespace Labyrinth.BL.Player
{
    public interface IPlayer
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
