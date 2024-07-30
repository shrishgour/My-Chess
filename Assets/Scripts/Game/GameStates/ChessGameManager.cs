using Core.StateMachine;

namespace Game
{
    public class ChessGameManager : StateMachine<ChessGameManager>
    {
        private ChessBoard chessBoard;

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