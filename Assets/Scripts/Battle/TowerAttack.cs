using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public enum ETypeTarget {
        ON_ENTER_AREA,
        MAX_HP,
    }
    public ETypeTarget target = ETypeTarget.ON_ENTER_AREA;
    public GameObject enemy;
    public GameObject enemyOnWait;
    public List<GameObject> enemyOnArea = new List<GameObject>();
    private float cooldown;
    public int damadge;
    public float maxCooldown = 2f;
    public GameObject bullet;
    public bool areaOfEffect = false;
    public Animator animator;
    public Animator TowerAnimator;
    void Start()
    {
        cooldown = 0f;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if(TowerAnimator)
        {
            TowerAnimator.Play("Attack");
        }

        if(areaOfEffect)
        {
            AttackMultipleEnemy();
        }
        else
        {
            AttackSingleEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!enemyOnArea.Contains(collision.gameObject))
        {
            enemyOnArea.Add(collision.gameObject);
        }

        // verificamos si ya hay un enemigo targeteando
        if(enemy == null)
        {
            enemy = collision.gameObject;
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        enemyOnWait = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(enemy == null)
        {
            return;
        }

        if(enemyOnArea.Contains(collision.gameObject))
        {
            enemyOnArea.Remove(collision.gameObject);
        }

        if(collision.gameObject.GetInstanceID() == enemy.GetInstanceID())
        {
            enemy = enemyOnWait;
        }
    }

    private void AttackSingleEnemy()
    {
        if(enemy == null)
        {
            return;
        }

        // Verificamos si es necesario atacar con un target en especifico.
        if(target == ETypeTarget.MAX_HP && enemyOnArea.Count > 0)
        {
            int maxLife = enemyOnArea[0].GetComponent<Enemy>().life;
            GameObject enemy = enemyOnArea[0];
            foreach (var e in enemyOnArea)
            {
                int elife = e.GetComponent<Enemy>().life;
                if(elife > maxLife){
                    enemy = e;
                    maxLife = e.GetComponent<Enemy>().life;
                }
            }
        }

        // instanciamos el disparo hacia el enemigo
        Quaternion qt = Quaternion.FromToRotation(Vector3.up, enemy.transform.position - transform.position);
        GameObject instanceArrow = Instantiate(bullet, transform.position, qt);
        instanceArrow.GetComponent<Bullet>().damadge = damadge;
        instanceArrow.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(enemy.transform.position - transform.position) * 400f);
        Destroy(instanceArrow, 1f);

        cooldown = maxCooldown;
    }

    private void AttackMultipleEnemy()
    {
        cooldown = maxCooldown;
        foreach (var enemy in enemyOnArea.ToArray())
        {
            animator.Play("Attack");
            enemy.GetComponent<Enemy>().Hurt(damadge);
        }
    }
}
