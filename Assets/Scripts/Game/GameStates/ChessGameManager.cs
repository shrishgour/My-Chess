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
        private int currentTurnIndex = 0;

        public string GameType => gameType;

        public BasePlayer[] players = new BasePlayer[2];
        public BasePlayer CurrentPlayer => players[currentTurnIndex];
        public BasePlayer LocalPlayer => players[0];

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

        public void SetNextTurn()
        {
            currentTurnIndex++;
            if (currentTurnIndex >= players.Length)
            {
                currentTurnIndex = 0;
            }
        }
    }
}