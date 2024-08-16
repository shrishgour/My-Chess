using System.Collections.Generic;
using Game.Attribute;
using UnityEngine;

namespace Game
{
    public class Piece : MonoBehaviour
    {
        [StringListDropdown("Game.TeamColor")] [SerializeField]
        private string pieceColor;

        private Vector2Int position;

        protected ChessBoard board;

        public string PieceColor => pieceColor;

        public Vector2Int Position => position;
        public int x => position.x;
        public int y => position.y;

        public void Init(ChessBoard board)
        {
            this.board = board;
        }

        public void SetPosition(Vector2Int position)
        {
            this.position = position;
        }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public virtual List<Vector2Int> GetAvailableMoves()
        {
            return null;
        }
    }
}