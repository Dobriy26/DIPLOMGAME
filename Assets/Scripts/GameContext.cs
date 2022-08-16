using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameContext : MonoBehaviour, IGameContext
{
    [SerializeField] private UIView[] _views;
    private UIView _currentView;

    public static IGameContext Instance { get; private set; }

    public void ShowView(string viewName)
    {
        var tweener = _currentView.Hide();
        tweener.onComplete += () =>
        {
            _currentView = _views.First(v => v.ViewName == viewName);
            _currentView.Show();
        };
    }

    public void HideView()
    {
        _currentView.Hide();
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    

    private void Start()
    {
        _currentView = _views.First(v => v.ViewName == nameof(StartGame));
        _currentView.Show();
    }

}

