using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    public static int Keys { get; set; }

    protected override void OnPickup(Collider2D collision)
    {
        Keys++;
    }
}
