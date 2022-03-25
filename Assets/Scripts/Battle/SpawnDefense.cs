using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDefense : MonoBehaviour
{
  public GameObject simpleTower;
  public GameObject ufoTower;
  public GameObject catapultTower;
  public Outline cardSimpleTower;
  public Outline cardUfoTower;
  public Outline cardCatapultTower;
  public GameObject towerSelected;

  private void Start()
  {
    SelectTower("SIMPLE_TOWER");
  }

  public void BuildTower()
  {
    int coins = towerSelected.GetComponent<Tower>().coins;
    if (GameManager.instance.coins < coins)
      return;

    GameManager.instance.SubstractCoins(coins);
    GameManager.instance.addTower();
    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    position.z = 0;
    Instantiate(towerSelected, position, Quaternion.identity);
  }

  public void SelectTower(string tower)
  {

    cardSimpleTower.enabled = false;
    cardUfoTower.enabled = false;
    cardCatapultTower.enabled = false;

    switch (tower)
    {
      case "SIMPLE_TOWER":
        towerSelected = simpleTower;
        cardSimpleTower.enabled = true;
        break;
      case "UFO_TOWER":
        towerSelected = ufoTower;
        cardUfoTower.enabled = true;
        break;
      case "CATAPULT_TOWER":
        towerSelected = catapultTower;
        cardCatapultTower.enabled = true;
        break;
    }
  }
}
