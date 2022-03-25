using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 10;
    public int coins = 50;
    public float velocity = 1f;
    public int positionMesh;
    public bool isBoss = false;
    public Vector3 nextMesh;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        positionMesh = 0;
        nextMesh = GameManager.instance.navMesh[positionMesh+1].transform.position;
        transform.position = GameManager.instance.navMesh[positionMesh].transform.position;
    }

    private void Update()
    {
        MoveToMesh();
    }
    private void MoveToMesh()
    {
        transform.position += Vector3.Normalize(nextMesh - transform.position) * Time.deltaTime * velocity;
        
        if(Vector3.Distance(nextMesh, transform.position) < 0.1f)
        {
            positionMesh += 1;
            if(positionMesh >= GameManager.instance.navMesh.Length -1 )
            {
                GameManager.instance.GameOver();
                //Destroy(gameObject);
                return;
            }
            nextMesh = GameManager.instance.navMesh[positionMesh+1].transform.position;
            // verificamos si es necesario rotar el enemigo
            spriteRenderer.flipX = nextMesh.x < transform.position.x;
        }
    }   

    public void Hurt(int damadge)
    {
        life -= damadge;
        if( life <= 0)
        {
            Destroy(gameObject);
            GameManager.instance.coins += coins;
            GameManager.instance.enemyKills += 1;
            if(isBoss)
            {
                GameManager.instance.Victory();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Hurt(collision.gameObject.GetComponent<Bullet>().damadge);
            Destroy(collision.gameObject);
        }
        
    }
}

