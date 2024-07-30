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
            stateMachine.ChangeState(new EvaluationState(stateMachine));
            return base.Init();
        }
    }
}