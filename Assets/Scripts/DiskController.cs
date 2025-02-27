﻿using DualPantoFramework;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SpeechIO;
using System.Threading.Tasks;

public class DiskController : MonoBehaviour {

	private Rigidbody rb;
	private int p1Score;
	private int p2Score;
	private bool gameOver;
	private bool gamePaused;
	private LowerHandle lHandle;
	private UpperHandle uHandle;
	private SpeechOut speechOut;

	public GameObject p1;
	public GameObject p2;
	public float timeLeft;
	public Text scoreText;
	public Text timeText;
	public Text gameOverText;
	public Text timeUpText;
	public GameObject goalCanvas;
	public GameObject gameOverCanvas;
	public GameObject pauseMenuCanvas;
	

	async void Start ()
	{
		
	}

	public async Task ActivateDisk()
	{
		gameOver = false;
		gamePaused = false;

		gameOverCanvas.SetActive(false);
		pauseMenuCanvas.SetActive(false);
		goalCanvas.SetActive(false);

		rb = GetComponent<Rigidbody>();

		p1Score = 0;
		p2Score = 0;

		p1 = GameObject.FindWithTag("P1");
		p2 = GameObject.FindWithTag("P2");

		setScoreText();

		timeText.text = "TIME LEFT: " + timeLeft.ToString("f1");
		gameOverText.text = "";
		timeUpText.text = "";

		lHandle = GameObject.Find("Panto").GetComponent<LowerHandle>();
		uHandle = GameObject.Find("Panto").GetComponent<UpperHandle>();
		//await lHandle.MoveToPosition(new Vector3(0,1,-3.52f), 3, true);
		//await lHandle.MoveToPosition(gameObject.transform.position, 5, true);
		//lHandle.Freeze();
		await lHandle.SwitchTo(gameObject, 20f);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!gameOver)
			{
				if(!gamePaused)
					PauseMenu();
				else
					UnPauseMenu();
			}
		}

		//timeLeft -= Time.deltaTime;

		//timeText.text = "TIME LEFT: " + timeLeft.ToString("f1");

		//if ( timeLeft < 0 )
		//{
		//	Time.timeScale = 0;
		//	gameOverCanvas.SetActive(true);
		//	timeUpText.text = "TIME'S UP!";
		//	if(p1Score > p2Score)
		//	{
		//		gameOverText.text = "BLUE WINS!";
		//	}
		//	else if(p1Score < p2Score)
		//	{
		//		gameOverText.text = "RED WINS!";
		//	}
		//	else
		//	{
		//		gameOverText.text = "IT'S A DRAW!";
		//	}
		//}
	}

	void OnCollisionEnter(Collision other)
	{
		//if (other.gameObject.CompareTag ("Player1Goal"))
		//{
		//	goalCanvas.SetActive(true);
		//	Time.timeScale = 0;
		//	StartCoroutine(ResetPos(false));
		//	p2Score += 1;
		//	setScoreText();
		//}

		//if (other.gameObject.CompareTag ("Player2Goal"))
		//{
		//	goalCanvas.SetActive(true);
		//	Time.timeScale = 0;
		//	StartCoroutine(ResetPos(true));
		//	p1Score += 1;
		//	setScoreText();
		//}
	}

	void setScoreText()
	{
		scoreText.text = "SCORE\nBLUE: " + p1Score + "\nRED: " + p2Score;
		if (p1Score >= 3)
		{
			gameOver = true;
			Time.timeScale = 0;
			gameOverCanvas.SetActive(true);
			gameOverText.text = "BLUE WINS!";
		}
		if (p2Score >= 3)
		{
			gameOver = true;
			Time.timeScale = 0;
			gameOverCanvas.SetActive(true);
			gameOverText.text = "RED WINS!";
		}
	}

	private IEnumerator ResetPos(bool p1Scored)
    {
        yield return new WaitForSecondsRealtime(2);

		if(!gameOver)
		{
			Time.timeScale = 1;
		}
		
		goalCanvas.SetActive(false);

		rb.velocity = new Vector3(0,0,0);
		p1.transform.position = new Vector3(0, 0.05f, -5);
		p2.transform.position = new Vector3(0, 0.05f, 5);

		if(p1Scored)
		{
			rb.transform.position = new Vector3(0,0,2);
		}
		else
		{
			rb.transform.position = new Vector3(0,0,-2);
		}
    }

	public void PauseMenu()
	{
		gamePaused = true;
		Time.timeScale = 0;
		pauseMenuCanvas.SetActive(true);
	}

	public void UnPauseMenu()
	{
		gamePaused = false;
		Time.timeScale = 1;
		pauseMenuCanvas.SetActive(false);
	}

}
