using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    private CharacterController superman;
    // Start is called before the first frame update
    void Start()
    {
        superman = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //計算和玩家的距離
        Vector3 dir = Player.transform.position- transform.position;



        //move
        superman.Move(dir.normalized * -5f * Time.deltaTime);
    }
  
}
