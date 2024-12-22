using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    bool isInRest = true;
    public bool touchingWall=false;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag(("Player")))
        {
            isInRest = false;
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
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
    void Update()
    {
        if(isInRest)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y),0);
        }
    }
}
