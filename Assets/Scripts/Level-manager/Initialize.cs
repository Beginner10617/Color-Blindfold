using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    LoadSaveSys gameManager;
    String initLevel;
    GameObject Level0;
    GameObject player;
    [SerializeField] Vector3 offset;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<LoadSaveSys>();
        initLevel = gameManager.LoadLevel(); 
        if(initLevel == "DefaultLevel") initLevel = gameManager.transform.GetChild(0).gameObject.name;
        Level0 = GameObject.Find(initLevel);
        transform.position = Level0.transform.position + offset;
        foreach(Transform child in gameManager.transform)
        {
            if(child.gameObject.name != initLevel)  child.gameObject.SetActive(false);
        }
        foreach(Transform child in Level0.transform)
        {
            if(child.gameObject.name == "Start")
            {
                player.GetComponent<PixelPerfectMovement>().checkPoint = child;
                player.GetComponent<PixelPerfectMovement>().Start();
                //player.transform.position = child.position;
            }
        }
    }
}
