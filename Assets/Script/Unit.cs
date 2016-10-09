using UnityEngine;
using System.Collections;

public class Unit : Entity
{
    public GameObject barrier;

    protected Rigidbody2D rigid;
    public Unit target;
    protected Vector2 posNow;
    protected Vector2 posNext;


    //unit stat
    public bool isDead;
    protected float speed;
    protected float attackRange;
    protected float hpNow;
    protected float hpMax;
    protected float hpRegen;
    protected float hpRegenMult;
    protected float skillNow;
    protected float skillMax;
    protected float skillRegen;
    protected float skillRegenMult;
    protected float phyAtk;
    protected float phySkill;
    protected float phyDef;
    protected float lastAtkTime;
    protected float atkSpeed;
    protected float atkMovePeriod;
    protected float atkFreezePeriod;
    protected float atkBackPeriod;

    protected bool isBreak;
    protected float breakTime;
    protected float breakPeriod;
    protected float lastSkillOneTime;
    protected float skillOneCd;
    protected bool team;

    static public bool isSameTeam(Unit a, Unit b)
    {
        return a.team == b.team;
    }

    virtual protected void Start()
    {
        breakTime = -100;
        target = null;
        posNow = transform.position;
        posNext = posNow;
        rigid = GetComponent<Rigidbody2D>();
        state = "moving";
        StartCoroutine(main());
    }

    virtual protected IEnumerator moving() {
        while (true) {
            posNow = transform.position;
            Vector2 dir = posNext - posNow;
            Vector2 move = Vector2.zero;
            float dist = dir.magnitude;
            dir.Normalize();

            if (target != null && targetInRange())
            {
                changeState("attackFreeze");
                yield break;
            }
            else if (Time.deltaTime * speed > dist)
            {
                move = dist / Time.fixedDeltaTime * dir;
            }
            else if (posNow != posNext)
            {
                move = speed * dir;
            }
            else
            {
                move = Vector2.zero;
            }
            rigid.velocity = move;
            yield return null;
        }
    }


    virtual protected IEnumerator attackMove()
    {
        Unit atkTarget = target;
        while (stateTime() < atkMovePeriod)
        {
            rigid.velocity = Vector2.zero;
            if (target == null || target != atkTarget)
            {
                changeState("moving");
                yield break;
            }
            yield return null;
        }
        rigid.velocity = Vector2.zero;
        autoAttack();
        lastAtkTime = Time.time;
        changeState("attackFreeze");
    }

    virtual protected IEnumerator attackFreeze()
    {
        Unit atkTarget = target;
        if (atkTarget == null) {
            changeState("moving");
            yield break;
        }
        while (Time.time <= lastAtkTime + atkFreezePeriod)
        {
            rigid.velocity = Vector2.zero;
            yield return null;
        }

        while (Time.time <= lastAtkTime + atkBackPeriod || stateTime() < atkMovePeriod)
        {
            rigid.velocity = Vector2.zero;
            if (target == null || target != atkTarget || !targetInRange())
            {
                changeState("moving");
                yield break;
            }
            yield return null;
        }
        changeState("attackMove");
    }

    virtual protected void Update()
    {
        atkBackPeriod = 1f / atkSpeed * 0.85f;
        atkMovePeriod = 1f / atkSpeed * 0.15f;
        atkFreezePeriod = 1f / atkSpeed * 0.20f;

        if (!isDead)
        {
            hpNow += hpRegen * hpRegenMult * Time.deltaTime;
            if (hpNow > hpMax) hpNow = hpMax;

            if (Time.time < breakTime + breakPeriod)
            {
            }
            else
            {
                skillNow += skillRegen * skillRegenMult * Time.deltaTime;
                if (skillNow > skillMax) skillNow = skillMax;
            }
        }
    }

    public void phyDamage(Unit enemy, float enemyPhyAtk, float enemyPhySkill){
        if (Time.time < breakTime + breakPeriod)
        {
            if (hpNow > enemyPhyAtk)
            {
                hpNow -= enemyPhyAtk;
            }
            else {
                hpNow = 0;
                isDead = true;
            }
        }
        else{
            if (skillNow > enemyPhySkill)
            {
                skillNow -= enemyPhySkill;
            }
            else {
                skillNow = 0;
                breakTime = Time.time;
            }
        }
    }

    public float getHpNow(){
        return hpNow;
    }

    public float getHpMax()
    {
        return hpMax;
    }

    public float getSkillNow()
    {
        return skillNow;
    }

    public float getSkillMax()
    {
        return skillMax;
    }

    bool targetInRange()
    {
        return Vector3.Distance(transform.position, target.transform.position) <= attackRange;
    }

    protected virtual void autoAttack(){
        target.GetComponent<Unit>().phyDamage(GetComponent<Unit>(), phyAtk, phySkill);
    }

    protected IEnumerator fastRegen()
    {
        float sreg = skillRegenMult;
        skillRegenMult = 1.5f;
        yield return new WaitForSeconds(3f);
        skillRegenMult = sreg;
    }
    protected IEnumerator fastAtk() {
        float aspd = atkSpeed;
        atkSpeed = 1.2f;
        yield return new WaitForSeconds(3f);
        atkSpeed = aspd;
    }
}

