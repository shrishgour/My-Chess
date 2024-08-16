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
        private ProcessMove boardMoveProcesser;

        private GameObject currentTile;
        private int ranks;
        private int files;
        private string turnColor;

        public Tile[,] tileMap;
        public Dictionary<string, Vector2Int> idToTileMap = new();
        public Piece[,] pieceGrid;
        public float TileScale { private set; get; }
        public string TurnColor => turnColor;
        public int Ranks => ranks;
        public int Files => files;

        public Action<Vector2Int> OnTileClicked;

        public ChessBoard()
        {
            boardLayout = ConfigRegistry.GetConfig<BoardLayout>();
            boardProperties = ConfigRegistry.GetConfig<BoardProperties>();
            pieceConfig = ConfigRegistry.GetConfig<PieceConfig>();

            boardInput = new ChessBoardInput(this);
            boardMoveProcesser = new ProcessMove(this);
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
            ranks = boardLayout.Data[STARTLAYOUT].rows;
            files = boardLayout.Data[STARTLAYOUT].columns;
            tileMap = new Tile[files, ranks];
            pieceGrid = new Piece[files, ranks];

            for (var i = 0; i < ranks; i++)
            {
                for (var j = 0; j < files; j++)
                {
                    var position = new Vector3(i * TileScale + TileScale / 2, 0, j * TileScale + TileScale / 2);
                    currentTile = Object.Instantiate(boardTile, position, Quaternion.identity, board);
                    currentTile.name = $"[ {i} , {j} ]";
                    currentTile.GetComponent<MeshRenderer>().material = (i + j) % 2 == 0
                        ? boardProperties.data.blackTileMaterial
                        : boardProperties.data.whiteTileMaterial;

                    SetupTileMapAndIDs(i, j, position);
                }
            }
        }

        private void SetupTileMapAndIDs(int i, int j, Vector3 position)
        {
            tileMap[i, j] = new Tile()
            {
                position = position,
                highlight = currentTile.transform.GetChild(0).gameObject
            };

            idToTileMap[GetChessPieceID(i, j)] = new Vector2Int(i, j);

            string GetChessPieceID(int x, int y)
            {
                if (x >= 0 && x < files && y >= 0 && y < ranks)
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
            var file = 0;
            var rank = boardLayout.Data[STARTLAYOUT].rows - 1;

            foreach (var pieceChar in fenArray)
            {
                var pieceFound = pieceConfig.Data.TryGetValue(pieceChar.ToString(), out var pieceData);

                if (pieceFound)
                {
                    var position = tileMap[file, rank].position;
                    var currentPiece = Object.Instantiate(pieceData.piecePrefab.gameObject,
                        position, Quaternion.identity, pieceHolder);
                    pieceGrid[file, rank] = currentPiece.GetComponent<Piece>();
                    pieceGrid[file, rank].Init(this);
                    pieceGrid[file, rank].SetPosition(new Vector2Int(file, rank));
                    file++;
                }
                else
                {
                    var pieceInt = Convert.ToInt32(pieceChar);
                    if (pieceInt is >= 48 and <= 56)
                    {
                        file += pieceInt - 48;
                    }
                    else if (pieceInt == 47)
                    {
                        rank--;
                        file = 0;
                    }
                }
            }
        }

        public void HighlightAvailableSquares(List<Vector2Int> availableSquares)
        {
            foreach (var square in availableSquares)
            {
                tileMap[square.x, square.y].highlight.SetActive(true);
            }
        }

        public void ResetHighlightSquares()
        {
            for (var i = 0; i < ranks; i++)
            {
                for (var j = 0; j < files; j++)
                {
                    tileMap[i, j].highlight.SetActive(false);
                }
            }
        }

        public bool CheckSquareOnBoard(Vector2Int position)
        {
            var isOnBoard = position.x > -1 && position.x < files &&
                            position.y > -1 && position.y < ranks;

            return isOnBoard;
        }

        public void SetTrunColor(string turnColor)
        {
            this.turnColor = turnColor;
        }
    }
}