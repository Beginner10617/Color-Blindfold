using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PixelPerfectMovement : MonoBehaviour
{
    public List<Vector2> inputBuffer;
    KeyCode moveUpKey = KeyCode.UpArrow;
    KeyCode moveDownKey = KeyCode.DownArrow;
    KeyCode moveLeftKey = KeyCode.LeftArrow;
    KeyCode moveRightKey = KeyCode.RightArrow;
    Vector2 currentPosition, nextPosition;
    RaycastHit2D rayHitInfo;
    public bool isMoving;
    public float speed;
    public float unitLength;
    public LayerMask layerOfWalls;
    public LayerMask layerOfBounds;
    public Transform checkPoint;
    public void Start()
    {
        inputBuffer = new List<Vector2>();
        transform.position = checkPoint.position;
        currentPosition = (Vector2) transform.position;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("MovableBlock"))
        {
            if(other.gameObject.GetComponent<MovableBlock>().touchingWall)
            {
                isMoving=false;
                inputBuffer = new List<Vector2>();
            }
        }
    }
    void Update()
    {
        //Input Handling
        if(Input.GetKeyDown(moveUpKey))     
            inputBuffer.Add(new Vector2( 0, 1));
        if(Input.GetKeyDown(moveRightKey))
            inputBuffer.Add(new Vector2( 1, 0));
        if(Input.GetKeyDown(moveLeftKey))
            inputBuffer.Add(new Vector2(-1, 0));
        if(Input.GetKeyDown(moveDownKey))
            inputBuffer.Add(new Vector2( 0,-1));
        
        if(inputBuffer.Count == 0)  return;
        //Checking for walls
        if(!isMoving)
        {
            rayHitInfo = Physics2D.Raycast(transform.position, inputBuffer[0], unitLength, layerOfWalls | layerOfBounds);
            if(rayHitInfo.collider != null)
            {
                inputBuffer.RemoveAt(0);
                return;
            }
        }
        //Moving
        nextPosition = currentPosition + inputBuffer[0] * unitLength;
        if((Vector2) transform.position == nextPosition)
        {
            currentPosition = nextPosition;
            inputBuffer.RemoveAt(0);
            isMoving = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            isMoving = true;
        }
    }
}
