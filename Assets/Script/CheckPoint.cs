using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public BoxCollider boxCollider;
    
    //give a color on the gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    //to be added, check point systeme
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("you passed a checkpoint");
    }
}
