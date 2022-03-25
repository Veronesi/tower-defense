using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public string sceneName;
    public bool canPlay;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(!canPlay)
        {
            spriteRenderer.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectScene()
    {
        if(!canPlay)
        {
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
