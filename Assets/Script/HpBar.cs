using UnityEngine;
using System.Collections;

public class HpBar : Bar {

    protected override void updateValue()
    {
        value = unit.getHpNow();
        max = unit.getHpMax();
    }
}
