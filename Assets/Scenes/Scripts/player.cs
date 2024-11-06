using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 5.0f;
    private int lives = 3;
    private int score = 0;
    public float horizontalInput;
    public float verticalInput;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.Translate(new Vector3(horizontalInput, 0, 0) * Time.deltaTime * speed);

        if (transform.position.x > 11.5f || transform.position.x <= -11.5f) {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
       

        if (transform.position.y > 8.5f || transform.position.y <= -8.5f)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y * -1, 0);
        }

    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
}
