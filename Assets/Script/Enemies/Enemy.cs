using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    
    float HP { get; set; }
    float MP { get; set; }
    float DEF { get; set; }
    float ATK { get; set; }
    void move();

}