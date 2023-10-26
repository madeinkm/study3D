using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooAtObject : MonoBehaviour
{
    [SerializeField] Transform Target;

    

    
    void Update()
    {
        LookAtTarget();

    }

    private void LookAtTarget()
    {
        if (Target == null)
        {
            return;
        }

        transform.LookAt(Target);        
    }
}
