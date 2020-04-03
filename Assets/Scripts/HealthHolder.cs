using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHolder : MonoBehaviour {
  private List<Image> heartImages = new List<Image>();

  public Sprite fullHeart;
  public Sprite halfHeart;

  public Sprite emptyHeart;
  public Health playerHealth;
  // Start is called before the first frame update
  void Start() {
    foreach (Transform child in transform) {
      if (child.CompareTag("Heart")) {
        heartImages.Add(child.GetComponent<Image>());
      }
    }
    UpdateHealth();
  }

  void UpdateHealth() {
    for (int i = 0; i < heartImages.Count; i++) {
      if (2 * (i + 1) <= playerHealth.currentValue) {
        heartImages[i].sprite = fullHeart;
      } else if (2 * (i + 1) - 1 == playerHealth.currentValue) {
        heartImages[i].sprite = halfHeart;
      } else {
        heartImages[i].sprite = emptyHeart;
      }
    }
  }

  public void HandleSignal(string message, string arg) {
    if (message == "updateHealth") {
      UpdateHealth();
    }
  }
}
