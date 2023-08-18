using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameActive = true;
    public bool isFreezed1 = false;
    public bool isFreezed2 = false;
    public bool isFreezed3 = false;
    public bool isFreezed4 = false;
    public int freezedCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freezedCount == 10)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameActive = false;
        Debug.Log("GameOver Bruh");
    }
}
