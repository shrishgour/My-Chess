using System.Collections;
using Core.StateMachine;

namespace Game
{
    public class PlayerTurnState : State<ChessGameManager>
    {
        public PlayerTurnState(ChessGameManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            stateMachine.ChessBoard.SetTurnColor(stateMachine.ChessBoard.LocalPlayer.teamColor);

            stateMachine.ChessBoard.LocalPlayer.MakeMove(() =>
                stateMachine.ChangeState(new EvaluationState(stateMachine)));
            return base.Init();
        }
    }
}