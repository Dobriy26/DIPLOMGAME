using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener((() =>
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }));
        menuButton.onClick.AddListener((() =>
        {
            SceneManager.LoadScene("Scenes/MainScene");
        }));
    }

    private void OnDisable()
    {
        restartButton.onClick.RemoveAllListeners();
        menuButton.onClick.RemoveAllListeners();
    }
}
