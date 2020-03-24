using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject {
  public List<SignalListener> listeners = new List<SignalListener>();

  public void RegisterListener(SignalListener signalListener) {
    listeners.Add(signalListener);
  }

  public void DeregisterListener(SignalListener signalListener) {
    listeners.Remove(signalListener);
  }

  public void Raise(string message, string arg) {
    for (int i = listeners.Count - 1; i >= 0; i--) {
      listeners[i].OnSignalRaised(message, arg);
    }
  }
}