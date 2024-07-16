using System;
using UnityEngine;

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
        public GameObject piecePrefab;
    }
}