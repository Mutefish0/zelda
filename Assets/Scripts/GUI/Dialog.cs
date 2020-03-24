using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {
  // Start is called before the first frame update
  public GameObject dialogBox;
  private Text text;

  void Start() {
    text = dialogBox.transform.Find("Text").gameObject.GetComponent<Text>();
  }

  public void SignalHandler(string message, string arg) {
    if (message == "message") {
      ShowMessage(arg);
    } else if (message == "close") {
      CloseDialog();
    }
  }

  void ShowMessage(string message) {
    text.text = message;
    dialogBox.SetActive(true);
  }

  void CloseDialog() {
    dialogBox.SetActive(false);
  }
}
