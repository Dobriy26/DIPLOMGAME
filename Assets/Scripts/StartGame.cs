using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
	[SerializeField] private Button startButton;
	[SerializeField] private Button exitButton;
	[SerializeField] private Button settingButton;
	[SerializeField] private Button closeButton;
	[SerializeField] private GameObject settingMenu;
	[SerializeField] private Transform openSetting;
	[SerializeField] private Transform closeSetting;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject startTween;
	[SerializeField] private GameObject exitTween;
	[SerializeField] private GameObject settingTween;
	[SerializeField] private Camera mainCam;

	private void Awake()
	{
		mainCam.GetComponent<AudioSource>().volume = GlobalSettings.GetVolume();
		mainCam.GetComponent<AudioSource>().mute = GlobalSettings.GetMute();
	}

	private void Start()
	{
		startButton.transform.DOMoveY(startTween.transform.position.y, .5f);
		exitButton.transform.DOMoveY(exitTween.transform.position.y, .5f);
		settingButton.transform.DOMoveX(settingTween.transform.position.x, .5f);
	}

	
    private void OnEnable()
    {
		startButton.onClick.AddListener((() =>
		{
			mainMenu.transform.DOScale(0, .5f);
			GlobalSettings.SetVolume(mainCam.GetComponent<AudioSource>().volume);
			GlobalSettings.SetMute(mainCam.GetComponent<AudioSource>().mute);
			SceneManager.LoadScene("Scenes/SampleScene");
		}));
		exitButton.onClick.AddListener((() =>
		{
			Application.Quit();
		}));
		settingButton.onClick.AddListener((() =>
		{
			settingMenu.transform.DOMoveX(openSetting.transform.position.x, .5f);
		}));
		closeButton.onClick.AddListener((() =>
		{
			settingMenu.transform.DOMoveX(closeSetting.transform.position.x, .5f);
		}));
	}

	private void OnDisable()
    {
		startButton.onClick.RemoveAllListeners();
		exitButton.onClick.RemoveAllListeners();
		settingButton.onClick.RemoveAllListeners();
		closeButton.onClick.RemoveAllListeners();
	}
	
}
