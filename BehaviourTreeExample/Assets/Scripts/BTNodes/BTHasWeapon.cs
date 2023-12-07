using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTHasWeapon : BTCondition<bool>
{
    public BTHasWeapon(Blackboard _blackBoard) : base(_blackBoard)
    {
        condition = (bool hasWeapon) => { return hasWeapon; };
    }

    protected override bool GetComparedValue()
    {
        return blackboard.Get<bool>("HasWeapon");
    }
}
