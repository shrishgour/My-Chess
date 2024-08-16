using System.Collections;
using Core.StateMachine;

namespace Game
{
    public class OpponentTurnState : State<ChessGameManager>
    {
        public OpponentTurnState(ChessGameManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            stateMachine.ChessBoard.SetTrunColor(stateMachine.players[1].teamColor);
            stateMachine.players[1].MakeMove(() => stateMachine.ChangeState(new EvaluationState(stateMachine)));
            return base.Init();
        }
    }
}