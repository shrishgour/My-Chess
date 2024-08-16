using UnityEngine;

namespace Game
{
    public class ChessBoardInput
    {
        private ChessBoard board;

        public ChessBoardInput(ChessBoard board)
        {
            this.board = board;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    GetClickedTile(hit.point);
                }
            }
        }

        private void GetClickedTile(Vector3 clickPos)
        {
            var tilePosition = new Vector2Int(
                Mathf.FloorToInt(clickPos.x / board.TileScale),
                Mathf.FloorToInt(clickPos.z / board.TileScale));
            Debug.Log($"Grid Position {tilePosition.x} , {tilePosition.y} ");
            board.OnTileClicked?.Invoke(tilePosition);
        }
    }
}