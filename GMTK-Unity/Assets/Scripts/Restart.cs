using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject RestartButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            Destroy(GameObject.Find("EnemySpawners"));
            Destroy(GameObject.Find("EnemyContainer"));
            RestartButton.SetActive(true);
        }

        if (Input.GetKeyDown("r"))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
