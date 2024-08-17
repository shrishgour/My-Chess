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
                stateMachine.ChessBoard.players[0].teamColor = TeamColor.white;
                stateMachine.ChessBoard.players[1].teamColor = TeamColor.black;
            }

            foreach (var player in stateMachine.ChessBoard.players)
            {
                player.SetActivePieces(stateMachine.ChessBoard.GetPiecesWithColor(player.teamColor));
            }

            stateMachine.ChangeState(new PlayerTurnState(stateMachine));
            return base.Init();
        }
    }
}