using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Pawn : Piece
    {
        public override List<Vector2Int> GetAvailableMoves()
        {
            var availableSquares = new List<Vector2Int>();
            var direction = PieceColor == TeamColor.white ? Vector2Int.up : Vector2Int.down;
            var takeDirections = new[] { new Vector2Int(1, direction.y), new Vector2Int(-1, direction.y) };
            var range = !hasMoved ? 2 : 1;

            for (var i = 1; i <= range; i++)
            {
                var targetSquare = Position + direction * i;
                if (!board.CheckSquareOnBoard(targetSquare))
                {
                    break;
                }

                if (board.pieceGrid[targetSquare.x, targetSquare.y] == null)
                {
                    availableSquares.Add(targetSquare);
                }

                if (board.pieceGrid[targetSquare.x, targetSquare.y] != null)
                {
                    break;
                }
            }

            foreach (var takeDirection in takeDirections)
            {
                var targetSquare = Position + takeDirection;
                if (!board.CheckSquareOnBoard(targetSquare))
                {
                    continue;
                }

                if (board.pieceGrid[targetSquare.x, targetSquare.y] != null &&
                    board.pieceGrid[targetSquare.x, targetSquare.y].PieceColor != PieceColor)
                {
                    availableSquares.Add(targetSquare);
                }
            }

            return availableSquares;
        }
    }
}