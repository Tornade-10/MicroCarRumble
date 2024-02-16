using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public CheckpointCount checkpointCount;

    //give a color on the gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        checkpointCount.PlayerGoThroughCheckpoint(this);
    }

    public void SetCheckpoint(CheckpointCount checkpointCount)
    {
        this.checkpointCount = checkpointCount;
    }
    
}
