using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
  public float range;
  public float speed;

  private Rigidbody2D myRigidbody;
  private Animator animator;

  private bool boomOnPlayer = false;

  private Transform playerTransform;
  Vector3 initialPos;
  // Start is called before the first frame update
  void Start()
  {
    initialPos = transform.position;
    animator = GetComponent<Animator>();

    myRigidbody = GetComponent<Rigidbody2D>();
  }

  // Update is called once per frame
  void Update()
  {

    if (boomOnPlayer)
    {
      myRigidbody.MovePosition(playerTransform.position);
    }
    else if (Vector3.Distance(transform.position, initialPos) < range)
    {
      myRigidbody.MovePosition(transform.position - transform.up * speed * Time.deltaTime);
    }
    else
    {
      animator.SetBool("boom", true);
    }
  }

  public void OnBoomCompleted()
  {
    Destroy(gameObject);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      boomOnPlayer = true;
      playerTransform = other.transform;
      animator.SetBool("boom", true);
    }
  }

}
