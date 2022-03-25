using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EEnemy
{
    public enum EEnemyType {
        SIMPLE_ENEMY,
        TANK_ENEMY
    }

    public interface IEnemyProb {
        EEnemy enemy { get; set;}
        int prob {get; set;}
    };

    public static Dictionary<EEnemyType, int> dictionaryProb = new Dictionary<EEnemyType, int>()
    {
        {EEnemyType.SIMPLE_ENEMY, 1000},
        {EEnemyType.TANK_ENEMY, 100}
    };

    public static Dictionary<EEnemyType, GameObject> dictionaryPrefab = new Dictionary<EEnemyType, GameObject>()
    {
        {EEnemyType.SIMPLE_ENEMY, Resources.Load("Prefabs/Enemies/Enemy") as GameObject},
        {EEnemyType.TANK_ENEMY, Resources.Load("Prefabs/Enemies/TankEnemy") as GameObject}
    };
}
