using UnityEngine;

public class BTSpotTarget : BTBaseNode
{
    public override string displayName => "Spotted Target";

    private ISpottable spottable;
    private bool spotted;
    private GameObject thisGameObject;

    public BTSpotTarget(Blackboard _blackBoard, ISpottable _spottable, bool _spotted) : base(_blackBoard)
    {
        spottable = _spottable;
        spotted = _spotted;
        thisGameObject = blackboard.Get<GameObject>("ControllerObject");
    }

    public override TaskStatus OnEnter()
    {
        if (spottable == null)
        {
            Debug.LogError("Spottable is null in BTSpotTarget.");
            return TaskStatus.Failed;
        }

        if (thisGameObject == null)
        {
            Debug.LogError("ThisGameObject is null in BTSpotTarget.");
            return TaskStatus.Failed;
        }

        spottable.Spot(thisGameObject, spotted);
        Debug.Log("Target spotted!");
        return base.OnEnter();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
