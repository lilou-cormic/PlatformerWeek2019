using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPickup : Item
{
    protected override void OnPickup(Collider2D collision)
    {
        Player.SetLightActive(true);
    }
}
