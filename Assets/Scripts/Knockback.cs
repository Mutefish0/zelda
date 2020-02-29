using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
  public float knockTime;
  public float thrust;
  // Start is called before the first frame update

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Enemy"))
    {
      Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
      if (enemy != null)
      {
        Vector2 diff = enemy.transform.position - transform.position;
        enemy.isKinematic = false;
        enemy.AddForce(diff.normalized * thrust, ForceMode2D.Impulse);
        StartCoroutine(KnockCo(enemy));
      }
    }
  }

  private IEnumerator KnockCo(Rigidbody2D enemy)
  {
    yield return new WaitForSeconds(knockTime);
    enemy.velocity = Vector2.zero;
    enemy.isKinematic = true;
  }

}
