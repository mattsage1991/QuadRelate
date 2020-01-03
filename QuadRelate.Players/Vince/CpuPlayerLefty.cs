using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerLefty : CpuPlayerBase
    {
        public CpuPlayerLefty(PlayerInitializer initializer) : base(initializer)
        {
        }

        public override string Name => "Lefty";

        public override int NextMove(Board board, Counter colour)
        {
            return board.AvailableColumns()[0];
        }
    }
}