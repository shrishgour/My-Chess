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
            stateMachine.ChessBoard.SetNextTurn();

            if (stateMachine.ChessBoard.CurrentPlayer is Player)
            {
                stateMachine.ChangeState(new PlayerTurnState(stateMachine));
            }
            else
            {
                stateMachine.ChangeState(new OpponentTurnState(stateMachine));
            }

            return base.Init();
        }
    }
}