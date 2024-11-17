using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Enemy1;
    public GameObject Cloud;
    public GameObject Coin;
    public GameObject powerup;

    public AudioClip powerUp;
    public AudioClip powerDown;

    public int cloudSpeed;

    private bool isPlayerAlive;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI restartText;
    public TextMeshProUGUI powerupText;

    private int score;
    private int lives;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemy1", 1f, 3f);
        InvokeRepeating("CreateCoin", 2f, 6f);
        StartCoroutine(CreatePowerup());
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        lives = 3;
        livesText.text = "Lives: " + lives;
        isPlayerAlive = true;
        cloudSpeed = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    void CreateEnemy1()
    {
        Instantiate(Enemy1, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180)); 
    }

    IEnumerator CreatePowerup()
    {
        Instantiate(powerup, new Vector3(Random.Range(-9f,9f),7.5f,0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3f, 6f));
        StartCoroutine(CreatePowerup());
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

    public void GameOver()
    {
        isPlayerAlive = false;
        CancelInvoke();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);
        cloudSpeed = 0;
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R) && isPlayerAlive == false)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdatePowerupText(string whichPowerup)
    {
        powerupText.text = whichPowerup;

    }
    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUp, Camera.main.transform.position);
    }
    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDown, Camera.main.transform.position);
    }
}
