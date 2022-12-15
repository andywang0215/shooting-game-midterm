using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosscome : MonoBehaviour
{
    public GameObject bosscome;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Boss", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Boss()
    {
        Instantiate(bosscome, new Vector3(-1.2f, 2, 35), Quaternion.identity);
    }
}
