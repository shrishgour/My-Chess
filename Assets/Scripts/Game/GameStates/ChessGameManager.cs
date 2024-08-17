using Core.StateMachine;
using Game.Attribute;
using UnityEngine;

namespace Game
{
    public class ChessGameManager : StateMachine<ChessGameManager>
    {
        [SerializeField] [StringListDropdown("Game.GameType")]
        private string gameType;

        private ChessBoard chessBoard;
        public string GameType => gameType;


        public ChessBoard ChessBoard
        {
            get { return chessBoard ??= new ChessBoard(); }
        }

        private void Start()
        {
            BeginGame();
        }

        private void Update()
        {
            ChessBoard.Update();
        }

        private void BeginGame()
        {
            ChangeState(new BeginState(this));
        }
    }
}