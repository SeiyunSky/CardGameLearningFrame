using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendManaGA : GameAction
{
     public int Amout { get; set; }

    public SpendManaGA(int amout)
    {
        Amout = amout;
    }
}
