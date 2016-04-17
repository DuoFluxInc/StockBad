using UnityEngine;
using System.Collections;

public class NTDoorController : MonoBehaviour
{
    public Animator animator;

    public void OnTriggerExit(Collider other)
    {
        if (!ValidateCollider(other)) return;
        if (!isAnimatorStay()) return;
        animator.Play("Close");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!ValidateCollider(other)) return;
        if (!isAnimatorStay()) return;
        animator.Play("Open");
    }

    private bool isAnimatorStay()
    {
        if (animator != null) return true;
        Debug.LogError("Animator is not found!");
        return false;
    }

    private bool ValidateCollider(Collider collider)
    {
        return collider.tag == "Player" || collider.tag == "phyObject";
    }
}
