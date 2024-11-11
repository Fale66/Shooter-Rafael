using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Enemy1;
    public GameObject Cloud;
    public GameObject Coin; 

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private int score;
    private int lives;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemy1", 1f, 3f);
        InvokeRepeating("CreateCoin", 1f, 5f);
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        lives = 3;
        livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy1()
    {
        Instantiate(Enemy1, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180)); 
    }

    void CreateCoin()
    {
        Instantiate(Coin, new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 4f), 0), Quaternion.identity);
    }
    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(Cloud, transform.position, Quaternion.identity);
        }
        
    }

    public void EarnScore(int newscore)
    {
        score = score + newscore;
        scoreText.text = "Score: " + score;
    }

    public void LoseALife(int newlife) 
    { 
        lives = lives + newlife;
        livesText.text = "Lives: " + lives;
    }
}
