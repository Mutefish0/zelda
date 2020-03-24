using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private Transform target;
  private Animator anim;
  private Vector3 homePos;

  private Rigidbody2D body;

  public GameObject logBallPrefab;
  public float attackRadius;
  public float chaseRadius; // 超出这个范围，怪物就自动归位

  public float chaseSpeed;
  public float health;

  private bool isWakeUpComplete = false;

  private bool isAttacking = false;

  private Action<Vector3> UpdateEnemyDirectionThrottle;

  public Enemy()
  {
    UpdateEnemyDirectionThrottle = Util.Throttle<Vector3>(UpdateEnemyDirection, 300);
  }

  // Start is called before the first frame update
  void Start()
  {
    target = GameObject.Find("Player").transform;
    anim = GetComponent<Animator>();
    homePos = transform.position;
    body = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {
    bool targetInRange = Vector3.Distance(homePos, target.position) < chaseRadius;
    bool targetInAttackRange = Vector3.Distance(transform.position, target.position) < attackRadius;
    bool backHome = transform.position == homePos;

    // 如果玩家出现且怪物是睡眠状态，则唤醒怪物
    if (targetInRange && !anim.GetBool("wakeUp"))
    {
      anim.SetBool("wakeUp", true);
    }

    // 如果玩家出现，并且不在攻击范围内，且是唤醒状态，则向玩家移动
    if (targetInRange && !targetInAttackRange && isWakeUpComplete)
    {
      Vector3 move = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
      body.MovePosition(move);
    }

    // 如果玩家出现，则调整怪物的方向
    if (targetInRange)
    {
      UpdateEnemyDirectionThrottle(target.position - transform.position);
    }

    // 如果玩家在攻击范围内，则设置怪物的刚体为静态，防止被玩家推着走
    if (targetInAttackRange)
    {
      body.bodyType = RigidbodyType2D.Static;
      if (!isAttacking)
      {
        StartCoroutine(AttackCo());
      }
    }
    else
    {
      body.bodyType = RigidbodyType2D.Dynamic;
    }

    // 如果玩家出了视野范围，并且怪物没归位，则怪物向原位置移动
    if (!targetInRange && !backHome)
    {
      Vector3 move = Vector3.MoveTowards(transform.position, homePos, chaseSpeed * Time.deltaTime);
      body.MovePosition(move);
      UpdateEnemyDirectionThrottle(homePos - transform.position);
    }

    // 如果玩家出了视野范围，且怪物归位了，并且怪物是唤醒状态，则进入休眠
    if (!targetInRange && backHome && anim.GetBool("wakeUp"))
    {
      anim.SetBool("wakeUp", false);
      isWakeUpComplete = false;
    }
  }

  IEnumerator AttackCo()
  {
    isAttacking = true;
    yield return new WaitForSeconds(1);
    Vector3 diff = target.position - transform.position;
    float theta = Vector3.SignedAngle(diff, Vector3.down, Vector3.back) / 180 * Mathf.PI;
    Quaternion rotation = new Quaternion(0, 0, Mathf.Sin(theta / 2), Mathf.Cos(theta / 2));
    Instantiate(logBallPrefab, transform.position, rotation);
    isAttacking = false;
  }

  void UpdateEnemyDirection(Vector3 move)
  {

    Vector3 dir = Util.calculateDirection(move);
    anim.SetFloat("moveX", dir.x);
    anim.SetFloat("moveY", dir.y);
  }

  public void OnWakeUpCompleted()
  {
    isWakeUpComplete = true;
  }
}
