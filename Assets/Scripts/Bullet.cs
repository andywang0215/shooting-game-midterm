using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 60f;
    private Rigidbody rb;
    public float atk = 50f;
    void Start()
    {
        // 往前飛
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞到的是子彈
        if (other.tag == "Monster"||other.tag == "Boss")
        {
            // 刪除自己
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
