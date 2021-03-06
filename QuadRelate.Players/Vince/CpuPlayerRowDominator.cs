﻿using System.Linq;
using QuadRelate.Contracts;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerRowDominator : IPlayer
    {
        private readonly IRandomizer _randomizer;

        public CpuPlayerRowDominator(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string Name => "Row Dominator";

        public int NextMove(Board board, Counter colour)
        {
            if (MovesHelper.TryGetBasicMove(board, colour, out var move))
                return move;

            var cells = CellsHelper.GetPlayableCells(board);
            var filteredCells = colour == Counter.Yellow ? cells.Where(c => c.Y % 2 == 0).ToList() : cells.Where(c => c.Y % 2 != 0).ToList();   // if Yellow prefer even, red odd.
            if (!filteredCells.Any())
            {
                filteredCells = cells.ToList();
            }

            return filteredCells[_randomizer.Next(filteredCells.Count)].X;
        }

        public void GameOver(GameResult result)
        {
        }
    }
}