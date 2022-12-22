using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCome : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(enemy, new Vector3(13.9f, 9.29f, 79.5f), Quaternion.identity);
            Instantiate(enemy, new Vector3(-6.2f, 9.6f, 79.5f), Quaternion.identity);
            Instantiate(enemy, new Vector3(-6.4f, 9.6f, 50.5f), Quaternion.identity);
            Instantiate(enemy, new Vector3(-15.4f, 9.6f, 50.5f), Quaternion.identity);
            Instantiate(enemy, new Vector3(14.4f, 9.6f, 50.5f), Quaternion.identity);
            Instantiate(enemy, new Vector3(8.3f, 9.6f, 62.2f), Quaternion.identity);
            Instantiate(enemy, new Vector3(-13f, 9.6f, 72f), Quaternion.identity);
            Instantiate(enemy, new Vector3(15.5f, 9.6f, 72f), Quaternion.identity);
            Instantiate(enemy, new Vector3(9.7f, 9.6f, 72f), Quaternion.identity);
            Instantiate(enemy, new Vector3(-15f, 9.6f, 79.7f), Quaternion.identity);
        }
    }
}
