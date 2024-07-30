using System;
using UnityEngine;

namespace Core.Config
{
    public class BoardProperties : BaseSingleConfig<BoardPropertiesData, BoardProperties>
    {
    }

    [Serializable]
    public class BoardPropertiesData : IConfigData
    {
        public string ID => nameof(BoardPropertiesData);
        public GameObject boardTile;
        public float tileScale;
        public Material blackTileMaterial;
        public Material whiteTileMaterial;
    }
}