using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class BasePlayer
    {
        public string teamColor;
        public List<Piece> activePieces;
        public bool isLocalPlayer;

        public void SetActivePieces(List<Piece> pieceList)
        {
            activePieces = pieceList;
        }

        public List<Vector2Int> GetAllAvailableMoves()
        {
            var availableSquares = new List<Vector2Int>();

            foreach (var piece in activePieces)
            {
                availableSquares.AddRange(piece.GetAvailableMoves());
            }

            return availableSquares;
        }

        public virtual void MakeMove(Action onComplete)
        {
        }
    }
}