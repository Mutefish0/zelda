using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
  private Animator anim;
  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Hit()
  {
    anim.SetTrigger("destroy");
  }

  public void OnDestroyNearlyComplete()
  {
    GetComponent<BoxCollider2D>().enabled = false;
  }

  public void OnDestroyComplete()
  {
    Destroy(gameObject);
  }
}
