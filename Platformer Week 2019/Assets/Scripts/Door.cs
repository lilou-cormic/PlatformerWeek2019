using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    protected override bool CanPickup(Collider2D collision)
    {
        return Key.Keys > 0;
    }

    protected override void OnPickup(Collider2D collision)
    {
        Key.Keys--;
    }
}
