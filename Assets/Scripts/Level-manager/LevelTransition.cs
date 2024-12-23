using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    bool transitioning = false;
    [SerializeField] Transform nextLevel;
    Transform currentLevel;
    [SerializeField] Vector3 offset = new Vector3(0.5f, 0, 0);
    [SerializeField] float speed = 7.5f;
    LoadSaveSys gameManager;
    GameObject frameOther, frameSelf;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<LoadSaveSys>();
        currentLevel = GameObject.FindWithTag("LevelHandle").transform;
        if(nextLevel != null)
        {
            nextLevel.gameObject.SetActive(false);
            foreach(Transform child in nextLevel)
            {
                if(child.gameObject.name == "Frame")
                {
                    frameOther = child.gameObject;
                    frameOther.SetActive(false);
                }
            }
            foreach(Transform child in transform.parent)
            {
                if(child.gameObject.name == "Frame")
                {
                    frameSelf = child.gameObject;
                    break;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            transitioning = true;
            if(nextLevel != null)   
            {
                nextLevel.gameObject.SetActive(true);
                frameSelf.SetActive(false);
                gameManager.SaveLevel(nextLevel.gameObject.name);
            }
            else
            {
                gameManager.SaveLevel("DefaultLevel");
            }
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
        {
            frameOther.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
