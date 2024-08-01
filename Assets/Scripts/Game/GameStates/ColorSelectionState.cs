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
            if (stateMachine.GameType == GameType.singlePlayer)
            {
                stateMachine.players[0].teamColor = TeamColor.white;
                stateMachine.players[1].teamColor = TeamColor.black;
            }

            stateMachine.ChangeState(new PlayerTurnState(stateMachine));
            return base.Init();
        }
    }
}