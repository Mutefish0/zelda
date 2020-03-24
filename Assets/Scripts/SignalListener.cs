using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalListener : MonoBehaviour {
  public SignalEvent signalEvent;
  public Signal signal;

  public void OnSignalRaised(string message, string arg) {
    signalEvent.Invoke(message, arg);
  }

  void Start() {
    signal.RegisterListener(this);
  }

}