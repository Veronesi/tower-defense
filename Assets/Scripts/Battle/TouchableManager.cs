using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchableManager : MonoBehaviour
{

  // Update is called once per frame
  void Update()
  {
    if (!Input.GetMouseButtonDown(0))
      return;

    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 2.0f, 1 << LayerMask.NameToLayer("Clickeable"));

    if (hit.collider == null)
        return;

    switch (hit.collider.gameObject.tag)
    {
        case "Button":
            hit.collider.gameObject.GetComponent<Button>().onClick.Invoke(); 
            break;
        case "Build":
            hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
            break;
        case "SpawnArea":
            GetComponent<SpawnDefense>().BuildTower();
            break;
        default:
            return;
    }
  }
}
