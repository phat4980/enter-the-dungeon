using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface Subscriber
{
    public abstract void OnNotify(string eventName,string addOrSub, int amount);
}
