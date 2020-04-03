using UnityEngine;

[CreateAssetMenu]
public class Health : ScriptableObject {
  public int initialValue = 10;
  public int _currentValue = 6;
  [SerializeField] private int baseValue = 6;

  public int currentValue { get { return _currentValue; } set { _currentValue = value; } }

  private void OnEnable() {
    _currentValue = baseValue;
  }
}
