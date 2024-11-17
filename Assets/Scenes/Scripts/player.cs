using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    private int lives;
    private int shooting;
    private bool hasShield;

    public GameManager gameManager;

    public GameObject bullet;
    public GameObject explosion;

    public GameObject thruster;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6.0f;
        lives = 3;
        shooting = 1;
        hasShield = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenSize || transform.position.y < -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (shooting)
            {
                case 1:
                    Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bullet, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0,0,30f));
                    Instantiate(bullet, transform.position + new Vector3(0f, 1, 0), Quaternion.identity);
                    Instantiate(bullet, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0, 0, -30f));
                    break;
            }
            
        }
    }

    public void LoseALife()
    {
        if (hasShield == false) 
        { 
            lives--; 
        }else if (hasShield == true)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LoseALife(1);
            gameManager.UpdatePowerupText("");
            gameManager.PlayPowerDown();
            hasShield = false;
            shield.gameObject.SetActive(false);
        }
        if (lives == 0)
        {
            gameManager.GameOver();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3);
        speed = 6f;
        thruster.gameObject.SetActive(false);
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    IEnumerator ShootingPowerDown()
    {
        yield return new WaitForSeconds(4f);
        shooting = 1;
        gameManager.UpdatePowerupText("");
        gameManager.PlayPowerDown();
    }

    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Coin")
        {
            gameManager.EarnScore(1);
            gameManager.PlayPowerUp();
            Destroy(whatIHit.gameObject);
        }
        else if (whatIHit.tag == "Powerup")
        {
            gameManager.PlayPowerUp();
            int powerupType = UnityEngine.Random.Range(1, 5);
            switch(powerupType)
            {
                case 1:
                    //speed
                    speed = 9f;
                    gameManager.UpdatePowerupText("Picked up Speed!");
                    thruster.gameObject.SetActive(true);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    //doubleshot
                    shooting = 2;
                    gameManager.UpdatePowerupText("Picked up Double Shot!");
                    StartCoroutine (ShootingPowerDown());
                    break;
                case 3:
                    //triple shot
                    shooting = 3;
                    gameManager.UpdatePowerupText("Picked up Triple Shot!");
                    StartCoroutine(ShootingPowerDown());
                    break;
                case 4:
                    //shield
                    gameManager.UpdatePowerupText("Picked up Shield!");
                    hasShield = true;
                    shield.gameObject.SetActive(true);
                    break;

            }
            Destroy(whatIHit.gameObject);
        }
    }
}
