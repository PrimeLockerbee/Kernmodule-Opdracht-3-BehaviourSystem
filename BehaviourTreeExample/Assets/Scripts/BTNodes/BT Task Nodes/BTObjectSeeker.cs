using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class BTObjectSeeker<T> : BTBaseNode where T : MonoBehaviour
{
    public override string s_DisplayName => "Seek " + s_TypeName;

    protected List<UtilityEval> uv_Evaluators;

    protected Transform t_ControllerTransform;
    protected NavMeshAgent nma_Agent;

    protected string s_TypeName;
    protected float mf_MaxDistance;

    public BTObjectSeeker(Blackboard _blackboard, float _maxdistance) : base(_blackboard)
    {
        s_TypeName = typeof(T).Name;
        mf_MaxDistance = _maxdistance;
        t_ControllerTransform = b_BlackBoard.Get<Transform>("ControllerTransform");
        nma_Agent = b_BlackBoard.Get<NavMeshAgent>("Agent");
    }

    public abstract void SetupEvaluators();

    public override TaskStatus OnEnter()
    {
        Collider[] collider = Physics.OverlapSphere(t_ControllerTransform.position, mf_MaxDistance);
        T[] objectsinrange = collider.Select(collider => collider.GetComponent<T>()).Where(t => t != null).ToArray();

        SetupEvaluators();
        float bestscore = 0f;
        T bestobject = null;
        
        foreach(T foundobject in objectsinrange)
        {
            b_BlackBoard.AddOrUpdate($"Current Search Item", foundobject);
            float normalizedscore = uv_Evaluators.Sum(e => e.GetNormalizedScore()) / uv_Evaluators.Count;

            if(normalizedscore > bestscore)
            {
                bestscore = normalizedscore;
                bestobject = foundobject;
            }
        }

        b_BlackBoard.RemoveEntry($"Current Search Item");

        if(bestobject == null)
        {
            b_BlackBoard.RemoveEntry($"Best{s_TypeName}Position");
            b_BlackBoard.RemoveEntry($"Best{s_TypeName}");

            return TaskStatus.Failed;
        }
        else
        {
            b_BlackBoard.AddOrUpdate($"Best{s_TypeName}Position", bestobject.transform.position);
            b_BlackBoard.AddOrUpdate($"Best{s_TypeName}", bestobject);
            return TaskStatus.Success;
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}
