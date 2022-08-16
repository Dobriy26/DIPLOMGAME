using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    private string _name;

    public abstract string ViewName { get; }
    public string Name { get => _name; protected set => _name = value; }

    protected void Initialize()
    {
        transform.localScale = Vector3.zero;
    }
        
    public Tweener Show()
    {
        gameObject.SetActive(true);
        return transform.DOMoveX(-1.47f, .5f);
    }

    public Tweener Hide()
    {
        transform.DOMoveX(2030f,.5f);
        var tweener = transform.DOScale(0f, .5f);
        tweener.onComplete += () => gameObject.SetActive(false);
        return tweener;
    }
}
