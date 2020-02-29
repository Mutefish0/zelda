using System;
using UnityEngine;

public class Util
{
  public static Vector3 calculateDirection(Vector3 move)
  {
    if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
    {
      return new Vector3(Mathf.Sign(move.x), 0, 0);
    }
    else
    {
      return new Vector3(0, Mathf.Sign(move.y), 0);
    }
  }

  public static Action<T> Throttle<T>(Action<T> func, long milliseconds)
  {
    long lastTime = 0;
    return (T arg) =>
    {
      long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
      if (currentTime > lastTime + milliseconds)
      {
        lastTime = currentTime;
        func(arg);
      }
    };
  }
}