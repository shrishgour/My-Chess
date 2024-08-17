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

            stateMachine.ChessBoard.SetupPlayers();

            stateMachine.ChangeState(new ColorSelectionState(stateMachine));
            return base.Init();
        }
    }
}