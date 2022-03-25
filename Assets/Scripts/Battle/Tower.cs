using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string cardName;
    public int coins;
    public Animator animator;
    public GameObject btnSell;

    public void SellTower()
    {
        GameManager.instance.AddCoins(coins / 2);
        GameManager.instance.RemoveTower();
        Destroy(gameObject);
    }

    public void ShowButtons()
    {
        btnSell.SetActive(!btnSell.activeSelf);
    }
}
