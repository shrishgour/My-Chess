using UnityEngine;

namespace Game
{
    public class ProcessMove
    {
        private ChessBoard board;
        private Piece currentPiece;
        private Tile currentTile;

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
            currentTile = board.tileMap[tilePos.x, tilePos.y];

            if (currentPiece != null)
            {
                ProcessPieceMove(currentTile);
            }
            else
            {
                currentPiece = currentTile.piece;
            }
        }

        private void ProcessPieceMove(Tile tile)
        {
            SetPieceOnTile(tile, currentPiece);
            currentPiece = null;
        }

        private void SetPieceOnTile(Tile tile, Piece piece)
        {
            tile.piece = piece;
            piece.SetPosition(tile.worldPosition);
        }
    }
}