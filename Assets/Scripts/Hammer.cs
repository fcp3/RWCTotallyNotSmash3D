using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon {

    protected override void Start()
    {
        base.Start();

        this.damage = 25;
    }
}
