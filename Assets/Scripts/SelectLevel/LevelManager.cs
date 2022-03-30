using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

      RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
      if (hit.collider == null)
      {
        return;
      }

      if (hit.collider.gameObject.tag != "SelectLevel")
      {
        return;
      }

      hit.collider.gameObject.GetComponent<SelectLevel>().SelectScene();
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
                _playerData.SetStateLevel(1);
            }
            return _playerData;
        }
        set
        {
            _playerData = value;
        }
    }
}
