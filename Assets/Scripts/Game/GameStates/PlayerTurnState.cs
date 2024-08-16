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
            stateMachine.ChessBoard.SetTrunColor(stateMachine.LocalPlayer.teamColor);

            stateMachine.LocalPlayer.MakeMove(() => stateMachine.ChangeState(new EvaluationState(stateMachine)));
            return base.Init();
        }
    }
}