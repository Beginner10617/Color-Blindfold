using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Portal otherPortal;
    public bool isWorking = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isWorking)
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PixelPerfectMovement>().nextPosition = otherPortal.transform.position;
                other.GetComponent<PixelPerfectMovement>().currentPosition = otherPortal.transform.position;
                other.GetComponent<PixelPerfectMovement>().inputBuffer.RemoveAt(0);
                other.GetComponent<PixelPerfectMovement>().isMoving = false;
            }
            // Teleport to the other portal
            otherPortal.isWorking = false;
            other.transform.position = otherPortal.transform.position;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isWorking = true;
    }
}
