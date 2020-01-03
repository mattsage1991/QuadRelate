﻿using System.Collections.Generic;
using System.Linq;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerVince : CpuPlayerBase
    {
        private const int _centreColumn = 3;

        public override string Name => "Invincible";

        public CpuPlayerVince(PlayerInitializer initializer) : base(initializer)
        {
        }

        public override int NextMove(Board board, Counter colour)
        {
            var availableMoves = board.AvailableColumns();

            // 1. If only one possible move - play it.
            if (availableMoves.Count == 1)
                return availableMoves[0];

            // 2. Start in centre.
            if (board[_centreColumn, 0] == Counter.Empty || board[_centreColumn, 1] == Counter.Empty)
                return _centreColumn;

            // 3. If there's a winning move - play it.
            foreach (var myMove in availableMoves)
            {
                var clone = board.Clone();
                clone.PlaceCounter(myMove, colour);
                if (clone.IsGameOver())
                    return myMove;
            }

            // 4. If there's a blocking move - play it.
            var opponent = colour.Invert();
            foreach (var opponentMove in availableMoves)
            {
                var clone = board.Clone();
                clone.PlaceCounter(opponentMove, opponent);
                if (clone.IsGameOver())
                    return opponentMove;
            }

            // 5. Check if opponent can win after this move.
            var reasonableMoves = new List<int>(availableMoves);
            foreach (var myMove in availableMoves)
            {
                var clone = board.Clone();
                clone.PlaceCounter(myMove, colour);
                foreach (var opponentMove in clone.AvailableColumns())
                {
                    var innerClone = clone.Clone();
                    innerClone.PlaceCounter(opponentMove, opponent);
                    if (innerClone.IsGameOver())
                        reasonableMoves.Remove(myMove);
                }
            }

            if (!reasonableMoves.Any())
            {
                // All moves lead to a potential loss.
                reasonableMoves = availableMoves;
            }

            // 6. ScoreEvaluator.
            var scores = new Dictionary<int, int>();
            foreach (var myMove in reasonableMoves)
            {
                var opponentTotal = 0;
                var clone = board.Clone();
                clone.PlaceCounter(myMove, colour);
                var myScore = ScoreEvaluator.GetScore(clone, colour);
                foreach (var opponentMove in clone.AvailableColumns())
                {
                    var innerClone = clone.Clone();
                    innerClone.PlaceCounter(opponentMove, opponent);
                    opponentTotal += ScoreEvaluator.GetScore(innerClone, opponent);
                }
                
                scores.Add(myMove, myScore - (opponentTotal / clone.AvailableColumns().Count));
            }

            //Debug.WriteLine(string.Join('.', scores));
            var bestScores = scores.Where(x => x.Value == scores.Values.Max());
            var bestMoves = bestScores.Select(x => x.Key).ToList();

            return GetMoveClosestToCentre(bestMoves);
        }

        public override void GameOver(GameResult result)
        {
            //if (result.Winner == _currentColour.Invert())
            //{
            //    Debug.WriteLine(string.Join('.', result.Moves));
            //}
        }

        private int GetMoveClosestToCentre(IList<int> moves)
        {
            for (var offset = 0; offset <= 3; offset++)
            {
                var move = _centreColumn - offset;
                if (moves.Contains(move))
                    return move;

                if (offset == 0)
                    continue;

                move = _centreColumn + offset;
                if (moves.Contains(move))
                    return move;
            }

            return Initializer.Randomizer.GetRandomItem(moves);
        }
    } 
}