using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public EnemyStatus status;
    private NavMeshAgent agent;
    public GameObject Player;
    public Transform superman;
    public GameObject bulletPrefab;
    private IEnumerator fireCoroutine;
    public Transform firePoint;
    private bool isAttacking = false;
    public float hp = 100;

    // Start is called before the first frame update


    public enum EnemyStatus
    {
        Idle, Chase, Attack, Escape, Heal, GetHeals
    }


    void Start()
    {
        status = EnemyStatus.Idle;
        agent = GetComponent<NavMeshAgent>();
        fireCoroutine = FireBullet();

    }
    // Update is called once per frame
    void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Monster");

        // 如果陣列長度為0 （陣列內沒東西）
        if (objs.Length == 0)
        {
            // 切換到下一關
            SceneManager.LoadScene("L2_rule");
        }
        float d = Vector3.Distance(transform.position, superman.transform.position);
           
            // Idle (閒置) 
            if (status == EnemyStatus.Idle)
            {
                // 往 Chase 判斷
                if (d < 10)
                {
                    status = EnemyStatus.Chase;
                    return;
                }

                

                // 行為
                Idle();
            }

            // Chase (追逐)
            if (status == EnemyStatus.Chase)
            {
                // 往 Idle 的判斷
                if (d > 12)
                {
                    status = EnemyStatus.Idle;
                    return;
                }
                if (d < 5)
                {
                   status = EnemyStatus.Attack;
                   return;
                }


            // 行為
            Chase();
            }

           

            // Escape (逃跑)
            if (status == EnemyStatus.Escape)
            {
                Escape();
            }

           if (status == EnemyStatus.Attack)
           {
            // 往 Chase 的判斷
              if (d > 5)
              {
                status = EnemyStatus.Chase;
                StopCoroutine(fireCoroutine);
                isAttacking = false;
                return;
              }

            

              // 行為
              Attack();
           }



    }

    // 狀態行為：閒置
    private void Idle()
        {
            agent.isStopped = true;
        }

        // 狀態行為：追逐
        private void Chase()
        {
            agent.isStopped = false;
            agent.SetDestination(superman.position);
        }

    // 狀態行為：攻擊
    private void Attack()
    {
        agent.isStopped = true;

        // 看向目標
        transform.LookAt(superman);

        // 只執行一次，開始重複射擊
        if (!isAttacking)
        {
            StartCoroutine(fireCoroutine);
            isAttacking = true;
        }
    }

    IEnumerator FireBullet()
    {
        // 無窮迴圈
        while (true)
        {
            // // 產生出子彈
            Instantiate(bulletPrefab, firePoint.transform.position, transform.rotation);

            // 停兩秒
            yield return new WaitForSeconds(1);
        }
    }

    private void Escape()
    {
        agent.isStopped = false;
        agent.SetDestination(superman.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞到的是子彈
        if (other.tag == "Bullet")
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



