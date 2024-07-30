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
            stateMachine.ChangeState(new OpponentTurnState(stateMachine));
            return base.Init();
        }
    }
}