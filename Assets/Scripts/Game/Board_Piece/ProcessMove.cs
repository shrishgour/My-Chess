using UnityEngine;

namespace Game
{
    public class ProcessMove
    {
        private ChessBoard board;
        private Piece currentPiece;
        private Piece selectedPiece;

        public ProcessMove(ChessBoard board)
        {
            this.board = board;

            board.OnTileClicked += TileClick;
        }

        ~ProcessMove()
        {
            board.OnTileClicked -= TileClick;
        }

        private void TileClick(Vector2Int tilePos)
        {
            board.ResetHighlightSquares();
            currentPiece = board.pieceGrid[tilePos.x, tilePos.y];

            if (selectedPiece == null)
            {
                if (currentPiece != null)
                {
                    if (currentPiece.PieceColor != board.TurnColor)
                    {
                        currentPiece = null;
                    }
                    else
                    {
                        selectedPiece = currentPiece;
                        var availableMoves = selectedPiece.GetAvailableMoves();
                        board.HighlightAvailableSquares(availableMoves);
                    }
                }
            }
            else
            {
                ProcessPieceMove(tilePos);
            }
        }

        private void ProcessPieceMove(Vector2Int tilePos)
        {
            selectedPiece.SetWorldPosition(board.tileMap[tilePos.x, tilePos.y].position);
            board.pieceGrid[selectedPiece.x, selectedPiece.y] = null;
            board.pieceGrid[tilePos.x, tilePos.y] = selectedPiece;
            selectedPiece.SetPosition(tilePos);
            selectedPiece = null;
        }
    }
}