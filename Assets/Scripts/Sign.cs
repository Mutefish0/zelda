using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {
  public string dialogContent;

  public Signal dialogSingal;
  private bool playerInRange;

  private bool isDialogOpen = false;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(KeyCode.Space) && playerInRange) {
      if (!isDialogOpen) {
        dialogSingal.Raise("message", dialogContent);
        isDialogOpen = true;
      } else {
        dialogSingal.Raise("close", "");
        isDialogOpen = false;
      }
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    playerInRange = true;
  }

  void OnTriggerExit2D(Collider2D other) {
    playerInRange = false;
    dialogSingal.Raise("close", "");
  }
}
