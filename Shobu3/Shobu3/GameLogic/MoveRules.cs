using System;
using System.Collections.Generic;
using System.Text;

namespace Shobu3.GameLogic
{
    public enum MoveRules
    {
        StraightLine,
        LegalDistance,
        AvoidsOwnPieces,
        PushesLessThan2Pieces,
        DoesNotPushWhilePassive,
        MatchesPassiveMoveWhileAggressive
    }
}