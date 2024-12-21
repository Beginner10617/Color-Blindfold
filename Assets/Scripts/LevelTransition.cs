using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    bool transitioning = false;
    [SerializeField] Transform nextLevel;
    Transform currentLevel;
    [SerializeField] Vector3 offset = new Vector3(0.5f, 0, 0);
    [SerializeField] float speed = 7.5f;
    void Start()
    {
        currentLevel = GameObject.FindWithTag("LevelHandle").transform;
        if(nextLevel != null)
            nextLevel.gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transitioning = true;
            if(nextLevel != null)   nextLevel.gameObject.SetActive(true);
        }
    }
    void Update()
    {
        if(!transitioning) return;
        if(nextLevel == null)
        {
            Debug.Log("No next level, game ended");
            //game end screen
            transform.parent.gameObject.SetActive(false);
            return;
        }
        if(currentLevel.position != nextLevel.position + offset)
            currentLevel.position = Vector2.MoveTowards(currentLevel.position, nextLevel.position + offset, speed * Time.deltaTime);
        else
            transform.parent.gameObject.SetActive(false);
    }
}
