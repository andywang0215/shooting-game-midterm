using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EggManControl : MonoBehaviour
{
    public EnemyStatus status;
    private NavMeshAgent agent;
    public GameObject Player;
    public Transform play;
    public GameObject bulletPrefab;
    private IEnumerator fireCoroutine;
    public Transform firePoint;
    private bool isAttacking = false;
    public float hp = 1500;

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

        float d = Vector3.Distance(transform.position, play.transform.position);

        // Idle (閒置) 
        if (status == EnemyStatus.Idle)
        {
            // 往 Chase 判斷
            if (d < 12)
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
            if (d > 15)
            {
                status = EnemyStatus.Idle;
                return;
            }
            if (d < 10)
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
            if (d > 10)
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
        agent.SetDestination(play.position);
    }

    // 狀態行為：攻擊
    private void Attack()
    {
        agent.isStopped = true;

        // 看向目標
        transform.LookAt(play);

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
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void Escape()
    {
        agent.isStopped = false;
        agent.SetDestination(play.position);
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
