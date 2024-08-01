using System.Collections;
using Core.StateMachine;

namespace Game
{
    public class BeginState : State<ChessGameManager>
    {
        public BeginState(ChessGameManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            stateMachine.ChessBoard.SetupBoard();
            stateMachine.ChessBoard.SetupPieces();

            InitPlayers();

            stateMachine.ChangeState(new ColorSelectionState(stateMachine));
            return base.Init();
        }

        private void InitPlayers()
        {
            stateMachine.players[0] = new Player();
            stateMachine.players[1] = new Bot();
        }
    }
}