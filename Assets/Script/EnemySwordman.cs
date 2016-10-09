using UnityEngine;
using System.Collections;

public class EnemySwordman : Enemy
{

    override protected void Start()
    {
        hpMax = 50;
        hpNow = 50;
        hpRegen = 1;
        hpRegenMult = 1;
        skillMax = 80;
        skillNow = 80;
        skillRegen = 5;
        skillRegenMult = 1;
        speed = 1;
        attackRange = 1;
        atkSpeed = 1f;
        phyAtk = 15;
        phySkill = 30;
        breakPeriod = 3;
        //enemy
        exp = 10;
        base.Start();
	}

}
