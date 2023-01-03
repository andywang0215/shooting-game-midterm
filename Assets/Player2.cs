using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    public float speed = 10;
    public Joystick joyStick;
    public Transform rightfirePoint;
    public Transform lefttfirePoint;
    public GameObject bulletPrefab;
    public float hp = 500;
    private CharacterController controller;

    private GameObject focusMonster;
    private GameObject focusBoss;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // 開始一直射擊的 Coroutine 函式
        StartCoroutine(KeepShooting());
    }

    void Update()
    {
        GameObject[] bm = GameObject.FindGameObjectsWithTag("BallMan");

        // 如果陣列長度為0 （陣列內沒東西）
        if (bm.Length == 0)
        {
            // 切換到下一關
            SceneManager.LoadScene("Menu");
        }
        

       
        GameObject[] BallMans = GameObject.FindGameObjectsWithTag("BallMan");

        float miniDist = 100;
        foreach (GameObject BallMan in BallMans)
        {
            // 計算距離
            float d = Vector3.Distance(transform.position, BallMan.transform.position);

            // 跟上一個最近的比較，有比較小就記錄下來
            if (d < miniDist)
            {
                miniDist = d;
                focusMonster = BallMan;
            }
        }






        // 取得虛擬搖桿輸入
        float h = joyStick.Horizontal;
        float v = joyStick.Vertical;

        // 合成方向向量
        Vector3 dir = new Vector3(h, 0, v);

        // 調整角色面對方向
        // 判斷方向向量長度是否大於 0.1（代表有輸入）
        if (dir.magnitude > 0.1f)
        {
            // 將方向向量轉為角度
            float faceAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;

            // 使用 Lerp 漸漸轉向
            Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
        }
        else
        {
            // 沒有移動輸入，並且有鎖定的敵人，漸漸面向敵人
            if (focusMonster)
            {
                var targetRotation = Quaternion.LookRotation(focusMonster.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
            }
            else if (focusBoss)
            {
                var targetRotation = Quaternion.LookRotation(focusBoss.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
            }
        }

        // 地心引力 (y)
        if (!controller.isGrounded)
        {
            dir.y = -9.8f * 30 * Time.deltaTime;
        }

        // 移動角色位置
        controller.Move(dir * speed * Time.deltaTime);


    }

    void Fire()
    {
        // 產生出子彈
        Instantiate(bulletPrefab, rightfirePoint.transform.position, transform.rotation);
        Instantiate(bulletPrefab, lefttfirePoint.transform.position, transform.rotation);
    }


    // 一直射擊的 Coroutine 函式
    IEnumerator KeepShooting()
    {
        while (true)
        {
            // 射擊
            Fire();

            // 暫停 0.5 秒
            yield return new WaitForSeconds(0.3F);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞到的是子彈
        if (other.tag == "MonsterBullet")
        {
            // 先取得子彈的攻擊力
            Bullet bullet = other.GetComponent<Bullet>();

            // 先扣血
            hp -= bullet.atk;

            // 如果沒血了，就刪除自己
            if (hp <= 0)
            {
                this.gameObject.SetActive(false);
                Destroy(gameObject);

            }
        }
    }

}
