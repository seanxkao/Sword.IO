using UnityEngine;
using System.Collections;

public class Player : Unit
{
    public MouseManager mouseManager;
    public float exp;
    public float expMax;
    public int level;

    override protected void Start()
    {
        hpMax = 100;
        hpNow = 100;
        hpRegen = 1;
        hpRegenMult = 1;
        skillMax = 150;
        skillNow = 150;
        skillRegen = 25;
        skillRegenMult = 1;
        lastAtkTime = Time.time;
        lastSkillOneTime = Time.time;
        skillOneCd = 10f;
        speed = 1.5f;
        attackRange = 1;
        phyAtk = 50;
        phySkill = 50;
        atkSpeed = 1f;
        breakPeriod = 3;
        team = false;
        base.Start();
	}
	
    override protected void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject clicked = mouseManager.hoveredObject;
            if (clicked != null && !Unit.isSameTeam(this, clicked.GetComponent<Unit>()))
            {
                target = clicked.GetComponent<Unit>();
            }
            else
            {
                target = null;
            }

            if (target == null)
            {
                posNext = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if(target!=null){
            posNext = target.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Q))// && Time.time > lastSkillOneTime + skillOneCd)
        {
            lastSkillOneTime = Time.time;
            Instantiate(barrier, this.transform, false);
            StartCoroutine(fastRegen());
        }

        if (Input.GetKeyDown(KeyCode.W))// && Time.time > lastSkillOneTime + skillOneCd)
        {
            StartCoroutine(fastAtk());
        }

        base.Update();
    }

    virtual public void addExp(int addexp){
        exp += addexp;
        while (exp > expMax) {
            exp -= expMax;
            expMax += level*10;
            level++;
            skillMax += 10;
            skillRegen += 2;
        }
    } 

}
