using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shift : MonoBehaviour
{
    public bool isOn = true;
    PixelPerfectMovement playerMovement;
    [SerializeField] float speed = 5f;
    [SerializeField] Vector3 destination = new Vector2(0, 0);
    void Start()
    {
        if(destination == new Vector3(0, 0, 0))
        {
            destination = transform.position + transform.up.normalized;
        }
    }
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = isOn;
    }
    IEnumerator ShiftRoutine(Transform obj)
    {
        while(true)
        {
            if(obj.position == destination)
            {
                break;
            }
            if(obj.CompareTag("MovableBlock"))
            {
                obj.position = Vector3.MoveTowards(obj.position, destination, speed * Time.deltaTime);
            }
            yield return null;
        }
        obj.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        if(obj.CompareTag("MovableBlock"))
        {
            obj.GetComponent<MovableBlock>().isInRest = true;
        }
        yield return null;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isOn) return;
        if (other.CompareTag("Player"))
        {
            playerMovement = other.gameObject.GetComponent<PixelPerfectMovement>();
            playerMovement.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            playerMovement.inputBuffer.Insert(1, transform.up.normalized);
            StartCoroutine(ShiftRoutine(other.transform));
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("MovableBlock"))
        {
            if(other.GetComponent<MovableBlock>().isInRest)
            {
                other.GetComponent<MovableBlock>().isInRest = false;
                other.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                StartCoroutine(ShiftRoutine(other.transform));
            }
        }
    }
}