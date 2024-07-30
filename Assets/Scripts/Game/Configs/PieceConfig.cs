using System;
using Game;

namespace Core.Config
{
    public class PieceConfig : BaseMultiConfig<PieceConfigData, PieceConfig>
    {
    }

    [Serializable]
    public class PieceConfigData : IConfigData
    {
        public string ID => pieceID;
        public string pieceID;
        public Piece piecePrefab;
    }
}