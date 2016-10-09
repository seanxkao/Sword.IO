using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Unit
{
    public List<Unit> enemies;
    public List<Unit> allies;

    protected float lastScanTime;

    public int exp;

    protected override void Start()
    {
        lastScanTime = -100;
        team = true;
        base.Start();
    }

    protected override void Update()
    {
        enemyAI();
        base.Update();
    }

    virtual protected void visionScan(){
        enemies.Clear();
        allies.Clear();
        Collider2D[] others = Physics2D.OverlapCircleAll((Vector2)transform.position, 5);
        foreach(Collider2D other in others){
            Unit unit = other.GetComponent<Unit>();
            if (unit == null)
                continue;
            if(!Unit.isSameTeam(this, unit)){
                enemies.Add(unit);
            }
            else{
                allies.Add(unit);
            }
        }
    }

    protected Unit getClosestEnemy()
    {
        Unit closestEnemy = null;
        float minDist = Mathf.Infinity;
        foreach (Unit enemy in enemies)
        {
            if (enemy != null)
            {
                Vector2 dir = enemy.transform.position - transform.position;
                float dist = dir.magnitude;
                if (dist < minDist)
                {
                    closestEnemy = enemy;
                    minDist = dist;
                }
            }
        }
        return closestEnemy;
    }

    protected virtual void enemyAI() {
        if (Time.time > lastScanTime + 5) {
            visionScan();
            if (target == null)
            {
                target = getClosestEnemy();
            }
            if (target != null)
            {
                posNext = target.transform.position;
            }
        }
    }


}
