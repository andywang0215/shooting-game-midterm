using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggManCome : MonoBehaviour
{
    public GameObject EggMan1;
    public GameObject EggMan2;
    public GameObject EggMan3;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EggMan", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EggMan()
    {
        Instantiate(EggMan1, new Vector3(-0.3f, 1.4f, 33.4f), Quaternion.identity);
        Instantiate(EggMan2, new Vector3(-12.5f, 1.4f, 35.56f), Quaternion.identity);
        Instantiate(EggMan3, new Vector3(11.1f, 1.4f, 35.56f), Quaternion.identity);

    }
}
