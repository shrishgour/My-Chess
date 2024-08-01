using UnityEngine;

namespace Game
{
    public class Tile
    {
        public GameObject tileObject;
        public Piece piece;
        public bool isWhite;
        public Vector2Int position;
        public Vector3 worldPosition;
    }
}