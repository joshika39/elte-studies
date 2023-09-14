using Labyrinth.BL.Base;
using Labyrinth.BL.Impl.Labyrinth;
using Labyrinth.BL.Impl.LabyrinthElements;
using Labyrinth.BL.Labyrinth;
using Labyrinth.BL.Player;

namespace Labyrinth.BL.Impl.Player
{
    internal class Player2D : ALabyrinthElement2D, IPlayer2D {

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }

        public Player2D(Guid id, string name, string email, IPosition2D position) : base(position)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
