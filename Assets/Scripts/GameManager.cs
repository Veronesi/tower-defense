using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public enum ETypeToWin {
        ENEMIES_KILL,
        GET_MONEYS,
    }
    public int level;
    public Transform[] navMesh;
    public static GameManager instance;
    [Header("Title Settings")]
    public GameObject gameOvertText;
    public GameObject victoryText;
    [Header("Win Settings")]
    public ETypeToWin needToWin;
    public int enemiesToKill;
    public int coinsToEarn;
    private bool isEnemyBossSpawn = false;
    public int enemyKills = 0;
    [Header("Enemies Settings")]
    public List<EEnemy.EEnemyType> enemies = new List<EEnemy.EEnemyType>();
    public EEnemy.EEnemyType bossEnemy;

    [Header("Towers Settings")]
    
    public int towerCount;
    public int maxTowerCount = 3;
    [Header("Coins Settings")]
    public int coins;
    public Text textCoins;
    public Text textTower;
    [Header("Director Settings")]    
    public GameObject director;

    private bool isGameOver = false;

    private int maxProbSpawnEnemy = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach (var enemy in enemies)
        {
            maxProbSpawnEnemy += EEnemy.dictionaryProb[enemy];
        }
        towerCount = 0;
        textTower.text = $"0/{maxTowerCount}";
        SpawnEnemy();
        StartCoroutine(PasiveCoins());
    }

    private void Update()
    {
        if(!isEnemyBossSpawn)
        {
            CheckSpawnEnemyBoss();
        }   
    }

    private void CheckSpawnEnemyBoss()
    {
        switch (needToWin)
        {
            case ETypeToWin.ENEMIES_KILL:
                if(enemyKills >= enemiesToKill)
                {
                    SpawnEnemyBoss();
                }
            break;
            case ETypeToWin.GET_MONEYS:
                if(coinsToEarn >= coins)
                {
                    
                    SpawnEnemyBoss();
                }
            break;
        }
    }

    private void SpawnEnemyBoss()
    {
        GameObject boss = Instantiate(EEnemy.dictionaryPrefab[bossEnemy], transform);
        boss.GetComponent<Enemy>().isBoss = true;
        isEnemyBossSpawn = true;
    }

    private void SpawnEnemy()
    {
        if(isEnemyBossSpawn){
            return;
        }
        int count = 0;
        int prob = Random.Range(0, maxProbSpawnEnemy+1);
        int _prob = prob;
        GameObject enemyForSpawn = EEnemy.dictionaryPrefab[EEnemy.EEnemyType.SIMPLE_ENEMY];
        while (prob >= 0)
        {
            if(EEnemy.dictionaryProb[enemies[count]] >= prob)
            {
                enemyForSpawn = EEnemy.dictionaryPrefab[enemies[count]];
                prob = -1;
            }else{
                prob -= EEnemy.dictionaryProb[enemies[count]];
            }
            count+=1;
        }

        Instantiate(enemyForSpawn, transform);
        Invoke("SpawnEnemy", Random.Range(3f, 6f));
    }

    public void SubstractCoins(int amount)
    {
            coins -= amount;
            textCoins.text = coins.ToString();
    }
    public void AddCoins(int amount)
    {
            coins += amount;
            textCoins.text = coins.ToString();
    }
    public void addTower()
    {
        towerCount += 1;
        textTower.text = $"{towerCount.ToString()}/{maxTowerCount.ToString()}";
    }

    public void RemoveTower()
    {
        towerCount -= 1;
        textTower.text = $"{towerCount.ToString()}/{maxTowerCount.ToString()}";
    }

    public void GameOver()
    {
        if(isGameOver)
            return;
        
        isGameOver = true;
        director.transform.Find("GameOver").GetComponent<PlayableDirector>().Play();
    }

    public void Victory()
    {
        director.transform.Find("Victory").GetComponent<PlayableDirector>().Play();
        playerData.SetStateLevel(level+1);
    }

    public void ReturnToWorlMap()
    {
        SceneManager.LoadScene("WorldMap");
    }

    IEnumerator PasiveCoins()
    {
        while(true) 
        { 
            coins += 10;
            textCoins.text = coins.ToString();
            yield return new WaitForSeconds(1f);
        }
    }
    private PlayerData _playerData;
    public PlayerData playerData
    {
        get
        {
            if (_playerData == null)
            {
                _playerData = new PlayerData();
            }
            return _playerData;
        }
        set
        {
            _playerData = value;
        }
    }
    
}