using System;
using Core.Config;
using UnityEngine;

namespace Game
{
    public class ChessBoard : MonoBehaviour
    {
        private const string STARTLAYOUT = "Startup";
        [SerializeField] private GameObject boardTile;
        [SerializeField] private Transform board;
        [SerializeField] private Transform pieceHolder;
        private BoardLayout boardLayout;
        private BoardProperties boardProperties;
        private GameObject currentTile;
        private PieceConfig pieceConfig;
        private float tileScale;

        private void Start()
        {
            boardLayout = ConfigRegistry.GetConfig<BoardLayout>();
            boardProperties = ConfigRegistry.GetConfig<BoardProperties>();
            pieceConfig = ConfigRegistry.GetConfig<PieceConfig>();

            SetupBoard();
            SetupPieces();
        }

        private void SetupBoard()
        {
            tileScale = boardProperties.data.tileScale;

            for (var i = 0; i < boardLayout.Data[STARTLAYOUT].rows; i++)
            {
                for (var j = 0; j < boardLayout.Data[STARTLAYOUT].columns; j++)
                {
                    var position = new Vector3(j * tileScale, 0, i * tileScale);
                    currentTile = Instantiate(boardTile, position, Quaternion.identity, board);
                    currentTile.name = $"[ {i} , {j}]";

                    currentTile.GetComponent<MeshRenderer>().material = (i + j) % 2 == 0
                        ? boardProperties.data.blackTileMaterial
                        : boardProperties.data.whiteTileMaterial;
                }
            }
        }

        private void SetupPieces()
        {
            var fenArray = boardLayout.Data[STARTLAYOUT].FEN.ToCharArray();
            var column = 0;
            var row = boardLayout.Data[STARTLAYOUT].rows - 1;

            foreach (var pieceChar in fenArray)
            {
                var pieceFound = pieceConfig.Data.TryGetValue(pieceChar.ToString(), out var pieceData);

                if (pieceFound)
                {
                    var position = new Vector3(column * tileScale, 0, row * tileScale);
                    var currentPiece = Instantiate(pieceData.piecePrefab, position, Quaternion.identity, pieceHolder);
                    column++;
                }
                else
                {
                    var pieceInt = Convert.ToInt32(pieceChar);
                    if (pieceInt is >= 48 and <= 56)
                    {
                        column += pieceInt - 48;
                    }
                    else if (pieceInt == 47)
                    {
                        row--;
                        column = 0;
                    }
                }
            }
        }
    }
}