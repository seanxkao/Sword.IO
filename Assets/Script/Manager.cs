using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Manager : MonoBehaviour {
    public MouseManager mouseManager;

    public Player playerLiterace;
    public Enemy enemySoider;
    public Enemy enemyArcher;
    public HpBar hpBar;
    public SkillBar skillBar;

    public Player player;
    public List<Enemy> enemies;

    protected void Start() {


        StartCoroutine(keepSpawningEnemy());
    }

    protected void Update() {
        for (int i = enemies.Count - 1; i >= 0; i--) {
            if (enemies[i].isDead) {
                player.addExp(enemies[i].exp);
                Destroy(enemies[i].gameObject);
                enemies.RemoveAt(i);
            }
        }
    }

    protected IEnumerator keepSpawningEnemy(){

        player = (Player)Instantiate(playerLiterace);
        player.mouseManager = mouseManager;
        HpBar hbar = (HpBar)Instantiate(hpBar, player.transform);
        hbar.unit = player;
        SkillBar sbar = (SkillBar)Instantiate(skillBar, player.transform);
        sbar.unit = player;
        while(true){
            addEnemy(enemySoider, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0));
            addEnemy(enemyArcher, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0));
            yield return new WaitForSeconds(3);
        }
    }

    public void addEnemy(Enemy enemy, Vector3 pos){
        Enemy e = (Enemy)Instantiate(enemy);
        enemies.Add(e);
        e.transform.position = pos;

        HpBar hbar = (HpBar)Instantiate(hpBar, e.transform);
        hbar.unit = e;
        SkillBar sbar = (SkillBar)Instantiate(skillBar, e.transform);
        sbar.unit = e;
    }

}
