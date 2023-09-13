using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.Player;

namespace Labyrinth.BL.Impl.Player
{
    public class Player2D : IPlayer2D {

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public IPosition2D Position { get; }

        public Player2D(Guid id, string name, string email, IPosition2D position)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }
    }
}
