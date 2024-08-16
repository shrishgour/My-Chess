using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Rook : Piece
    {
        private Vector2Int[] directions = new[] { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };

        public override List<Vector2Int> GetAvailableMoves()
        {
            var availableSquares = new List<Vector2Int>();

            foreach (var direction in directions)
            {
                for (var i = 1; i < board.Ranks; i++)
                {
                    var targetSquare = Position + direction * i;
                    if (!board.CheckSquareOnBoard(targetSquare))
                    {
                        break;
                    }

                    if (board.pieceGrid[targetSquare.x, targetSquare.y] == null ||
                        board.pieceGrid[targetSquare.x, targetSquare.y].PieceColor != PieceColor)
                    {
                        availableSquares.Add(targetSquare);
                    }

                    if (board.pieceGrid[targetSquare.x, targetSquare.y] != null)
                    {
                        break;
                    }
                }
            }

            return availableSquares;
        }
    }
}