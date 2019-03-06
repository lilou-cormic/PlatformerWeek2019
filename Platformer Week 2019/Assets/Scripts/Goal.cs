using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Item
{
    protected override bool CanPickup(Collider2D collision)
    {
        return !Player.IsGoingUp();
    }

    protected override void OnPickup(Collider2D collision)
    {
        LevelManager.Win();
    }
}
