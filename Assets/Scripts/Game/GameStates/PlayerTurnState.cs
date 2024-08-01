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
            stateMachine.players[0].MakeMove(() => stateMachine.ChangeState(new EvaluationState(stateMachine)));
            return base.Init();
        }
    }
}