using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// win & lose function
/// win & lose UI
/// countdown before start level
/// in game timer
/// star (highscore)
/// </summary>

public class GameController : MonoBehaviour {

    public PlayerController PC; // access player controller script

    public GameObject Player;
    public Transform playerPos; // position for lose particle to follow the player position
    
    
    public Text countDownText;
    public Image LevelClear; // win canvas
    public Image TryAgain; // lose canvas
    public GameObject threeStarImage, twoStarImage, oneStarImage; // star image
    public Animator oneStar, twoStar, threeStar; // animator for star win

    public int star;
    [SerializeField]
    private float gameTimer; //in game timer

    public bool gameTimerBool;
    bool countDownStart;
    float countDownTimer; // count down before game start
    


    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.SetActive(false);
        countDownTimer = 3.0f;
        countDownStart = true;
        LevelClear.gameObject.SetActive(false);
        TryAgain.gameObject.SetActive(false);
        threeStarImage.gameObject.SetActive(false);
        twoStarImage.gameObject.SetActive(false);
        oneStarImage.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        timerCountDown(); // count down before start game
        playGameTimer(); // in game timer
        
        if(PC.winBool == true)
        {
            Win(); // win function
        }

        if (PC.loseBool == true)
        {
            Vector3 pos = playerPos.transform.position;
            PC.loseParticle.transform.position = pos; // lose particle follow player's last pos
            Lose(); // lose function
        }
    }

    void timerCountDown()
    {
        if (countDownStart == true)
        {
            countDownTimer -= Time.deltaTime;
            countDownText.text = "" + Mathf.Round(countDownTimer);
            Player.SetActive(false);
        }

        if (countDownTimer <= 0)
        {
            countDownTimer = 0;
            countDownStart = false;
            countDownText.gameObject.SetActive(false);
            Player.SetActive(true);
            countDownStart = false;
        }
    }
    
    public void playGameTimer()
    {
        if (gameTimerBool == true)
        {
            gameTimer += Time.deltaTime;
        }
    }

    public void stopGameTimer()
    {
        gameTimerBool = false;
    }

    void Win()
    {
        PC.enabled = false;
        Player.SetActive(false);
        if(gameTimer < 7)
        {
            star = 3; // get 3 star if less than 7 secs
            StartCoroutine(winCanvas()); // display win canvas and star
        }

        if(gameTimer > 7 && gameTimer < 15)
        {
            star = 2; // get 2 star if less than 15 secs
            StartCoroutine(winCanvas());
        }

        if(gameTimer > 15)
        {
            star = 1; // get 1 star if more than 15 secs
            StartCoroutine(winCanvas());
        }
    }

    void Lose()
    {
        PC.enabled = false;
        Player.SetActive(false);
        star = 0;
        StartCoroutine(loseCanvas()); // display lose canvas and try again
    }

    IEnumerator winCanvas()
    {
        yield return new WaitForSeconds(0.5f);

        if(star == 3)
        {
            LevelClear.gameObject.SetActive(true);
            threeStarImage.gameObject.SetActive(true);
            threeStar.Play("3StarWin");
        }

        if(star == 2)
        {
            LevelClear.gameObject.SetActive(true);
            twoStarImage.gameObject.SetActive(true);
            twoStar.Play("2StarWin");
        }

        if(star == 1)
        {
            LevelClear.gameObject.SetActive(true);
            oneStarImage.gameObject.SetActive(true);
            oneStar.Play("1StarWin");
        }
        
    }

    IEnumerator loseCanvas()
    {
        yield return new WaitForSeconds(0.5f);

        TryAgain.gameObject.SetActive(true);
    }

}
