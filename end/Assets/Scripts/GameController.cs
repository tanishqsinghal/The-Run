using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GameController : MonoBehaviour {


	public GameObject gameOverPanel;

	public Text scoreText;
	int score = 0;

	public Text bestText;
	public Text currentText;

	private float heldTime = 0.0f;
	InterstitialAd interstitial;

	void Start () {
		RequestInterstitial();
	}
	
	// Update is called once per frame
	void Update () {
		heldTime += Time.deltaTime;
		if(heldTime >= 1){
			score += (int)heldTime * 5;
			heldTime -= (int)heldTime;
			scoreText.text = score.ToString ();
		}

		if (Input.GetKeyDown(KeyCode.Escape)) 
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1); 
	}

	void FixedUpdate()
	{
		if (score == 50) {
			Social.ReportProgress("CgkItKq_zYwPEAIQAg", 100, success => { });
		} else if (score == 100) {
			Social.ReportProgress("CgkItKq_zYwPEAIQAw", 100, success => { });
		} else if (score == 150) {
			Social.ReportProgress("CgkItKq_zYwPEAIQBA", 100, success => { });
		} else if (score == 200) {
			Social.ReportProgress("CgkItKq_zYwPEAIQBQ", 100, success => { });
		} else if (score == 250) {
			Social.ReportProgress("CgkItKq_zYwPEAIQBg", 100, success => { });
		} else if (score == 300) {
			Social.ReportProgress("CgkItKq_zYwPEAIQBw", 100, success => { });
		} else if (score == 350) {
			Social.ReportProgress("CgkItKq_zYwPEAIQCA", 100, success => { });
		} else if (score == 400) {
			Social.ReportProgress("CgkItKq_zYwPEAIQCQ", 100, success => { });
		} else if (score == 450) {
			Social.ReportProgress("CgkItKq_zYwPEAIQCg", 100, success => { });
		} else if (score == 500) {
			Social.ReportProgress("CgkItKq_zYwPEAIQCw", 100, success => { });
		}
	}

	public void GameOver()
	{
		Invoke ("ShowOverPanel", 2.0f);
	}

	public void showInterstitialAd()
	{
		//Show Ad
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
		}

	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-2389250670002713/3865220873";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
		}

	void ShowOverPanel()
	{
		scoreText.gameObject.SetActive (false);

		if(score > PlayerPrefs.GetInt("Best", 0))
		{
			PlayerPrefs.SetInt ("Best", score);
			if (Social.localUser.authenticated) {
				Social.ReportScore (100, "CgkItKq_zYwPEAIQAQ", (bool success) =>
					{
						if (success) {
							Debug.Log ("Update Score Success");

						} else {
							Debug.Log ("Update Score Fail");
						}
					});
			}
		}
		bestText.text = "HI : " + PlayerPrefs.GetInt ("Best", 0).ToString ();
		currentText.text = "CURRENT : "+ score.ToString();

		gameOverPanel.SetActive (true);
		showInterstitialAd ();
	}

	public void Restart()
	{
		//Application.LoadLevel (Application.loadedLevelName);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void IncreamentScore()
	{
		score += 5;

		scoreText.text = score.ToString ();
	}
}
