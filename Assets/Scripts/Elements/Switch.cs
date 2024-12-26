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
            if(device.GetComponent<Shift>())
                device.GetComponent<Shift>().isOn = deviceDefaultState;
            if(device.GetComponent<Laser>())
                device.GetComponent<Laser>().laserOn = deviceDefaultState;
        }
        else
        {
            if(device.GetComponent<Shift>())
                device.GetComponent<Shift>().isOn = !deviceDefaultState;
            if(device.GetComponent<Laser>())
                device.GetComponent<Laser>().laserOn = !deviceDefaultState;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        isOn = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isOn = false;
    }
}
