using System.Collections;
using Core.StateMachine;

namespace Game
{
    public class EndState : State<ChessGameManager>
    {
        public EndState(ChessGameManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            return base.Init();
        }
    }
}