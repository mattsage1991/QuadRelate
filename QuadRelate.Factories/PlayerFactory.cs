using System;
using QuadRelate.Contracts;
using QuadRelate.Players;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;

namespace QuadRelate.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly PlayerInitializer _initializer;

        public PlayerFactory(IRandomizer randomizer)
        {
            _initializer = new PlayerInitializer{Randomizer = randomizer};
        }

        public IPlayer CreatePlayer(string playerType)
        {
            switch (playerType)
            {
                case nameof(HumanPlayer): return new HumanPlayer();
                case nameof(CpuPlayerRandom): return new CpuPlayerRandom(_initializer);
                case nameof(CpuPlayerVince): return new CpuPlayerVince(_initializer);
                case nameof(CpuPlayerLefty): return new CpuPlayerLefty(_initializer);
                case nameof(CpuPlayer01): return new CpuPlayer01(_initializer);
                case nameof(CpuPlayer02): return new CpuPlayer02(_initializer);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), "That player does not exist");
            }
        }
    }
}
