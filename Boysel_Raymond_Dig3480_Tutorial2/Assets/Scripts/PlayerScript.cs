using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    public AudioSource AudioSource;
    public AudioClip musicone;
    public AudioClip musictwo;

    private int scoreValue;
    private int lives;
    private int counter;
    private bool facingRight = true;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        lives = 3;
        SetLivesText();
        Setscore();
        counter = 0;
        AudioSource.clip = musicone;
        AudioSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    private void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            Destroy(collision.collider.gameObject);
            scoreValue = scoreValue + 1;
            counter = counter + 1;
            Setscore();
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives = lives - 1;
            SetLivesText();
        }
        if (collision.collider.tag == "Pit")
        {
            lives = 0;
            SetLivesText();
        }

        if (scoreValue == 8)
        {
            winText.text = "You win! Game created by Raymond Boysel!";
            AudioSource.clip = musictwo;
            AudioSource.Play();
        }
        if (lives == 0)
        {
            winText.text = "You Lose! Game created by Raymond Boysel!";
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 0);
            }
        }
        if (lives == 0)
        {
            winText.text = "You Lose! Game created by Raymond Boysel!";
            Destroy(gameObject);
        }
        if ((scoreValue == 4) && (counter== 4))
        {
            counter = counter + 1;
            transform.position = new Vector2(100.0f, 0f);
            lives = 3;
            SetLivesText();
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
    void Setscore()
    {
        score.text = "Score: " + scoreValue.ToString();
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}