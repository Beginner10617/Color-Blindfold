using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectMovement : MonoBehaviour
{
    public List<Vector2> inputBuffer;
    KeyCode moveUpKey = KeyCode.UpArrow;
    KeyCode moveDownKey = KeyCode.DownArrow;
    KeyCode moveLeftKey = KeyCode.LeftArrow;
    KeyCode moveRightKey = KeyCode.RightArrow;
    Vector2 currentPosition, nextPosition;
    public float speed;
    public float unitLength;
    void Start()
    {
        inputBuffer = new List<Vector2>();
        transform.position = new Vector3(0,0,0);
        currentPosition = new Vector2(0,0);
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
        
        //Moving
        if(inputBuffer.Count == 0)  return;
        nextPosition = currentPosition + inputBuffer[0] * unitLength;
        if((Vector2) transform.position == nextPosition)
        {
            currentPosition = nextPosition;
            inputBuffer.RemoveAt(0);
        }
        else
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        
    }
}
