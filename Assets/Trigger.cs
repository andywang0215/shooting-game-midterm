using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private Door m_Door;
    private bool enterCollide = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Door = GameObject.Find("OpenDoor").GetComponent<Door>();

    }
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
        enterCollide = true;
        m_Door.OpenDoorMethod();
        
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("exit");
        enterCollide = false;
        m_Door.CloseDoorMethod();
    }



    // Update is called once per frame
    void Update()
    {
        
      


    }
}
