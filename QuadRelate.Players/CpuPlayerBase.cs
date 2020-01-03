using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Players
{
    public abstract class CpuPlayerBase : IPlayer
    {
        protected readonly PlayerInitializer Initializer;

        protected CpuPlayerBase(PlayerInitializer initializer)
        {
            Initializer = initializer;
        }

        public abstract string Name { get; }
        public abstract int NextMove(Board board, Counter colour);

        public virtual void GameOver(GameResult result)
        {
        }
    }
}