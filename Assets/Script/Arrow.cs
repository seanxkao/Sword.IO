using UnityEngine;
using System.Collections;

public class Arrow : Entity
{
    public GameObject sprite;
    public Unit self;
    public Unit enemy;
    public float phyAtk;
    public float skill;
    public float flySpeed;

	// Use this for initialization
	void Start () {
        state = "flying";
        StartCoroutine(main());
	}

    protected virtual IEnumerator flying()
    {
        while(enemy != null)
        {
            Vector2 dir = (Vector2)(enemy.transform.position - transform.position);
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x);
            if (dir.x != float.NaN)
                transform.Translate(dir * flySpeed * Time.deltaTime);
            if (angle != float.NaN)
                sprite.transform.eulerAngles = new Vector3(0, 0, angle * 180f / Mathf.PI);
            yield return null;
        }
        changeState("disappear");
        
    }

    protected virtual IEnumerator disappear()
    {
        //disappear animation
        yield return null;
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Landscape"))
        {
            Destroy(gameObject);
        }
        if (enemy && collider.gameObject == enemy.gameObject) {
            //do atk
            enemy.phyDamage(self, phyAtk, skill);
            Destroy(gameObject);
        }
    }
}
