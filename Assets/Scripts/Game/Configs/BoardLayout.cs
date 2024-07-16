using System;

namespace Core.Config
{
    public class BoardLayout : BaseMultiConfig<BoardLayoutData, BoardLayout>
    {
    }

    [Serializable]
    public class BoardLayoutData : IConfigData
    {
        public string ID => layoutID;
        public string layoutID;
        public int rows;
        public int columns;
        public string FEN;
    }
}