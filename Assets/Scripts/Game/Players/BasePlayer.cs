using System;
using System.Collections.Generic;

namespace Game
{
    public abstract class BasePlayer
    {
        public string teamColor;
        public List<Piece> activePieces;
        public bool isLocalPlayer;

        public virtual void MakeMove(Action onComplete)
        {
        }
    }
}