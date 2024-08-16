using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Knight : Piece
    {
        private Vector2Int[] offsets = new[]
        {
            new Vector2Int(-2, 1),
            new Vector2Int(-2, -1),
            new Vector2Int(-1, 2),
            new Vector2Int(-1, -2),
            new Vector2Int(2, 1),
            new Vector2Int(2, -1),
            new Vector2Int(1, 2),
            new Vector2Int(1, -2)
        };

        public override List<Vector2Int> GetAvailableMoves()
        {
            var availableSquares = new List<Vector2Int>();

            foreach (var offset in offsets)
            {
                var targetSquare = Position + offset;
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