using System.Collections;
using Core.StateMachine;

namespace Game
{
    public class ColorSelectionState : State<ChessGameManager>
    {
        public ColorSelectionState(ChessGameManager stateMachine) : base(stateMachine)
        {
        }

        public override IEnumerator Init()
        {
            stateMachine.ChangeState(new PlayerTurnState(stateMachine));
            return base.Init();
        }
    }
}