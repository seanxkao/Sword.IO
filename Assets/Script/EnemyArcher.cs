using UnityEngine;
using System.Collections;

public class EnemyArcher : Enemy
{
    public Arrow arrow;

    override protected void Start()
    {
        hpMax = 50;
        hpNow = 50;
        hpRegen = 1;
        hpRegenMult = 1;
        skillMax = 80;
        skillNow = 80;
        skillRegen = 4;
        skillRegenMult = 1;
        speed = 1;
        attackRange = 4;
        atkSpeed = 1.1f;
        phyAtk = 20;
        phySkill = 25;
        breakPeriod = 3;
        //
        exp = 12;
        base.Start();
    }

    protected override void autoAttack()
    {
        Arrow newArrow = (Arrow)Instantiate(arrow);
        newArrow.transform.position = transform.position;
        newArrow.enemy = target.GetComponent<Unit>();
        newArrow.phyAtk = phyAtk;
        newArrow.skill = phySkill;
        newArrow.flySpeed = 7;
        newArrow.self = this;
    }

}
