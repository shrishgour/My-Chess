using System.Collections;
using Core.StateMachine;

namespace Game
{
    public class EvaluationState : State<ChessGameManager>
    {
        public EvaluationState(ChessGameManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            stateMachine.ChangeState(new EndState(stateMachine));
            return base.Init();
        }
    }
}