using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float speed = 5;
  private Rigidbody2D myRigidbody;
  private Animator animator;

  private bool isAttacking = false;
  // Start is called before the first frame update
  void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (isAttacking)
    {
      return;
    }

    // move character
    float inputX = Input.GetAxis("Horizontal");
    float inputY = Input.GetAxis("Vertical");

    if (inputX != 0 || inputY != 0)
    {
      Vector3 move = new Vector3(inputX, inputY, 0) * speed * Time.deltaTime;
      Vector3 dir = Util.calculateDirection(move);
      float moveLength = Mathf.Abs(Mathf.Abs(move.x) > Mathf.Abs(move.y) ? move.x : move.y);

      animator.SetBool("moving", true);
      animator.SetFloat("moveX", dir.x);
      animator.SetFloat("moveY", dir.y);
      myRigidbody.MovePosition(transform.position + move.normalized * moveLength);
    }
    else
    {
      animator.SetBool("moving", false);
    }

    // attack
    if (Input.GetKeyDown(KeyCode.J))
    {
      animator.SetTrigger("attack");
      isAttacking = true;
    }
  }

  public void OnAttackComplete()
  {
    isAttacking = false;
  }
}
