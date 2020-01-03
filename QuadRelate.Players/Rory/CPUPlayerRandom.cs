using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Rory
{
    public class CpuPlayerRandom : CpuPlayerBase
    {
        public CpuPlayerRandom(PlayerInitializer initializer) : base(initializer)
        {
        }

        public override string Name => "The Randomizer";

        public override int NextMove(Board board, Counter colour)
        {
            return Initializer.Randomizer.GetRandomItem(board.AvailableColumns());
        }
    }
}