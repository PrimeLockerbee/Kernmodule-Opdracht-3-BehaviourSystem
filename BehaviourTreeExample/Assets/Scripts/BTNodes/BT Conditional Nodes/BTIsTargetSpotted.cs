using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIsTargetSpotted : BTCondition<bool>
{
    private IsSpotable is_Spottable;

    public BTIsTargetSpotted(Blackboard _blackBoard, IsSpotable _spottable) : base(_blackBoard)
    {
        is_Spottable = _spottable;
        sp_Condition = (bool isSpotted) => isSpotted;
    }

    protected override bool GetComparedValue()
    {
        return is_Spottable.b_IsSpotted;
    }
}
