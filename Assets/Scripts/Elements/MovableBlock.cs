using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    public bool isInRest = true;
    PixelPerfectMovement playerMovement;
    public bool touchingWall=false;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(("Player")))
        {
            playerMovement = other.gameObject.GetComponent<PixelPerfectMovement>();
            isInRest = false;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall")||other.gameObject.layer == LayerMask.NameToLayer("Bound"))
        {
            touchingWall=true;
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!other.gameObject.GetComponent<PixelPerfectMovement>().isMoving)
            {
                isInRest = true;
            }
            else
            {
                isInRest = false;
            }
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(("Player")))
        {
            isInRest = true;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            touchingWall=false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Shift") && !isInRest)
        {
            if(playerMovement != null)
            {
                if(playerMovement.inputBuffer.Count == 0) return;
                if(playerMovement.inputBuffer[0] + (Vector2)other.transform.up.normalized == Vector2.zero)
                {
                    playerMovement.inputBuffer.RemoveAt(0);
                    playerMovement.isMoving = false;
                    playerMovement.nextPosition = playerMovement.currentPosition;
                }


            }
        }
    }
    void Update()
    {
        if(isInRest)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y),0);
        }
    }
}
