using UnityEngine;
using System.Collections;

public class SkillBar : Bar
{

    protected override void updateValue()
    {
        value = unit.getSkillNow();
        max = unit.getSkillMax();
    }
}
