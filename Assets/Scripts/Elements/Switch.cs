using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject device;
    public bool deviceDefaultState = false;
    bool isOn = false;
    void Update()
    {
        if(!isOn)
        {
            device.GetComponent<Laser>().laserOn = deviceDefaultState;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        isOn = true;
        if(device.GetComponent<Laser>())
        {
            device.GetComponent<Laser>().laserOn = !deviceDefaultState;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
    }
}
