using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class menu : MonoBehaviour {

	public BannerView bannerView;

	void Start()
	{
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();

		SignIn();

		RequestBanner ();
	}

	public void SignIn()
	{
		Social.localUser.Authenticate(success => { });
	}

	public void ShowAchievements() {
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ShowAchievementsUI();
		}
		else {
			Debug.Log("Cannot show Achievements, not logged in");
		}
	}

	public void RequestBanner()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2389250670002713/5842493436";
		#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the bottom of the screen.
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}

	public void PlayGame()
	{
		bannerView.Destroy();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

		public void ShowLeaderboards() {
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
		PlayGamesPlatform.Instance.ShowLeaderboardUI();
		}
		else {
		Debug.Log("Cannot show leaderboard: not authenticated");
		}
		}

}
