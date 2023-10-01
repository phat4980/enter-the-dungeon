using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Publisher : MonoBehaviour
{
    private static List<Subscriber> subscribers = new List<Subscriber>();

    public static void Subscribe(Subscriber s)
    {
        subscribers.Add(s);
    }
    public static void Unsubscribe(Subscriber s)
    {
        subscribers.Remove(s);
    }
    public void notify(string detailChange, string addOrSub, int amount)
    {
        //foreach (Subscriber s in subscribers)
        //{
        //    s.OnNotify(detailChange);
        //}
        subscribers.ForEach((s) => { s.OnNotify(detailChange, addOrSub, amount); });
    }
}
