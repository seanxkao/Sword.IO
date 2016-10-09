using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Vision : MonoBehaviour {
    public Unit self;
    public List<Unit> enemies;
    public List<Unit> allies;

    void Start() {
        self = transform.parent.gameObject.GetComponent<Unit>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Unit other = collider.gameObject.GetComponent<Unit>();
        if (self!=null && other!=null)
        {
            if (!Unit.isSameTeam(self, other) && !enemies.Contains(other))
            {
                enemies.Add(collider.GetComponent<Unit>());
            }
            if (Unit.isSameTeam(self, other) && !allies.Contains(other))
            {
                allies.Add(collider.GetComponent<Unit>());
            }
        }

    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Unit other = collider.gameObject.GetComponent<Unit>();
        if (self != null && other != null)
        {
            if (!Unit.isSameTeam(self, other) && enemies.Contains(other))
            {
                enemies.Remove(collider.GetComponent<Unit>());
            }
            if (Unit.isSameTeam(self, other) && allies.Contains(other))
            {
                allies.Remove(collider.GetComponent<Unit>());
            }
        }
    }

    public Unit getClosestEnemy() {
        Unit closestEnemy = null;
        float minDist = Mathf.Infinity;
        foreach(Unit enemy in enemies){
            if (enemy != null)
            {
                Vector2 dir = enemy.transform.position - self.transform.position;
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
}
