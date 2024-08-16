using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class King : Piece
    {
        private Vector2Int[] directions = new[]
        {
            new Vector2Int(-1, 1),
            new Vector2Int(1, 1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, -1),
            Vector2Int.left,
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down
        };

        public override List<Vector2Int> GetAvailableMoves()
        {
            var availableSquares = new List<Vector2Int>();

            foreach (var direction in directions)
            {
                var targetSquare = Position + direction;
                if (!board.CheckSquareOnBoard(targetSquare))
                {
                    continue;
                }

                if (board.pieceGrid[targetSquare.x, targetSquare.y] == null ||
                    board.pieceGrid[targetSquare.x, targetSquare.y].PieceColor != PieceColor)
                {
                    availableSquares.Add(targetSquare);
                }
            }

            return availableSquares;
        }
    }
}