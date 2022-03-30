using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public int sceneLevel;
    public bool canPlay;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        canPlay = LevelManager.instance.playerData.GetStateLevel(sceneLevel);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(!canPlay)
        {
            spriteRenderer.color = Color.black;
        }
    }
    public void SelectScene()
    {
        if(!canPlay)
        {
            return;
        }
        SceneManager.LoadScene($"Level1_{sceneLevel}");
    }
}
