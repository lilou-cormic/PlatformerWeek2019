using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Item
{
    protected override void OnPickup(Collider2D collision)
    {
        LevelManager.Lose();
    }
}
