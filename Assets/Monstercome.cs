using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstercome : MonoBehaviour
{
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Monster", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Monster()
    {
       
            Instantiate(monster1, new Vector3(-0.3f,1.4f,33.4f), new Quaternion(0,90,0,0));
            Instantiate(monster2, new Vector3(-12.5f,1.4f,35.56f), new Quaternion(0, 90, 0, 0));
            Instantiate(monster3, new Vector3(11.1f, 1.4f, 35.56f), new Quaternion(0, 90, 0, 0));

    }
}
