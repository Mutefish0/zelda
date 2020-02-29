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

  public float attackRadius;
  public float chaseRadius; // 超出这个范围，怪物就自动归位

  public float chaseSpeed;
  public float health;

  private bool isWakeUpComplete = false;

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

    if (targetInRange && !anim.GetBool("wakeUp"))
    {
      anim.SetBool("wakeUp", true);
    }

    if (targetInRange && !targetInAttackRange && isWakeUpComplete)
    {
      Vector3 move = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
      body.MovePosition(move);
    }

    if (targetInRange)
    {
      UpdateEnemyDirectionThrottle(target.position - transform.position);
    }

    if (!targetInRange && !backHome)
    {
      Vector3 move = Vector3.MoveTowards(transform.position, homePos, chaseSpeed * Time.deltaTime);
      body.MovePosition(move);
      UpdateEnemyDirectionThrottle(homePos - transform.position);
    }

    if (!targetInRange && backHome && anim.GetBool("wakeUp"))
    {
      anim.SetBool("wakeUp", false);
      isWakeUpComplete = false;
    }
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
