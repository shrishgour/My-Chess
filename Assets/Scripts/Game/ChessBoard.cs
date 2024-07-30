using System;
using System.Collections.Generic;
using Core.Config;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class ChessBoard
    {
        private const string STARTLAYOUT = "Startup";
        private GameObject boardTile;
        private Transform board;
        private Transform pieceHolder;

        private BoardLayout boardLayout;
        private BoardProperties boardProperties;
        private PieceConfig pieceConfig;
        private ChessBoardInput boardInput;

        private GameObject currentTile;
        private int rows;
        private int columns;
        private Tile[,] tileMap;
        private Dictionary<string, Vector2Int> idToTileMap = new();

        public float TileScale { private set; get; }

        public ChessBoard()
        {
            boardLayout = ConfigRegistry.GetConfig<BoardLayout>();
            boardProperties = ConfigRegistry.GetConfig<BoardProperties>();
            pieceConfig = ConfigRegistry.GetConfig<PieceConfig>();

            boardInput = new ChessBoardInput(this);
            board = new GameObject("Board").transform;
            pieceHolder = new GameObject("PieceHolder").transform;
            boardTile = boardProperties.data.boardTile;
            TileScale = boardProperties.data.tileScale;
        }

        public void Update()
        {
            boardInput?.Update();
        }

        public void SetupBoard()
        {
            rows = boardLayout.Data[STARTLAYOUT].rows;
            columns = boardLayout.Data[STARTLAYOUT].columns;
            tileMap = new Tile[rows, columns];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    var position = new Vector3(j * TileScale + TileScale / 2, 0, i * TileScale + TileScale / 2);
                    currentTile = Object.Instantiate(boardTile, position, Quaternion.identity, board);
                    currentTile.name = $"[ {i} , {j} ]";
                    SetupTileMapAndIDs(i, j);

                    currentTile.GetComponent<MeshRenderer>().material = (i + j) % 2 == 0
                        ? boardProperties.data.blackTileMaterial
                        : boardProperties.data.whiteTileMaterial;
                }
            }
        }

        private void SetupTileMapAndIDs(int i, int j)
        {
            tileMap[i, j] = new Tile
            {
                tileObject = currentTile,
                isWhite = (i + j) % 2 != 0
            };

            idToTileMap[GetChessPieceID(i, j)] = tileMap[i, j].position = new Vector2Int(i, j);

            string GetChessPieceID(int x, int y)
            {
                if (x >= 0 && x < columns && y >= 0 && y < rows)
                {
                    var column = (char)('a' + x);
                    var row = y + 1;
                    return $"{column}{row}";
                }
                else
                {
                    Debug.LogError("Invalid grid coordinates");
                    return null;
                }
            }
        }

        public void SetupPieces()
        {
            var fenArray = boardLayout.Data[STARTLAYOUT].FEN.ToCharArray();
            var column = 0;
            var row = boardLayout.Data[STARTLAYOUT].rows - 1;

            foreach (var pieceChar in fenArray)
            {
                var pieceFound = pieceConfig.Data.TryGetValue(pieceChar.ToString(), out var pieceData);

                if (pieceFound)
                {
                    var position = new Vector3(column * TileScale + TileScale / 2, 0, row * TileScale + TileScale / 2);
                    var currentPiece = Object.Instantiate(pieceData.piecePrefab.gameObject,
                        position, Quaternion.identity, pieceHolder);
                    tileMap[row, column].piece = currentPiece.GetComponent<Piece>();
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