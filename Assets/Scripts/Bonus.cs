using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlackTailsUnityTools.Scripts.Utils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public BonusType BonusType;
    
    private Tween _vertical;
    private Tween _horizontal;

    private bool _active;

    [SerializeField] private Image _image;
    [SerializeField] private Sprite _regenSprite;
    [SerializeField] private Sprite _gold;
    [SerializeField] private Sprite _upgrade;

    private void Start()
    {
        StartCoroutine(BonusStart());
    }

    private IEnumerator BonusStart()
    {
        while (true)
        {
            Setup();
            yield return new WaitForSeconds(60);
        }
    }

    public void Setup()
    {
        if(_active)
            return;
        
        _active = true;
        transform.localPosition = new Vector3(0, 300, 0);
        BonusType = Enum.GetValues(typeof(BonusType)).Cast<BonusType>().ToList().GetRandomElement();
        
        _image.sprite = BonusType switch
        {
            BonusType.Regeneration => _regenSprite,
            BonusType.Gold => _gold,
            BonusType.Upgrade => _upgrade
        };
        
        Activate();
    }
    
    public void Activate()
    {
        _vertical = transform.DOMoveY(-1000, 14).OnComplete(End);
        _horizontal = transform.DOMoveX(transform.position.x+300, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public void OnClick()
    {
        switch (BonusType)
        {
            case BonusType.Regeneration:
                PlayerController.Instance.Regeneration(PlayerController.Instance.MaxHealth * 0.1f);
                break;
            case BonusType.Gold:
                PlayerController.Instance.AddReward(Math.Clamp((int)(PlayerController.Instance.Money * 0.5f), 50, 1000), 0);
                break;
            case BonusType.Upgrade:
                var lowestUpgrade = PlayerController.Instance.Upgrades.OrderBy(x => x.Level).First();
                PlayerController.Instance.Upgrade(lowestUpgrade.Type);
                break;
        }

        End();
    }

    public void End()
    {
        _vertical.Kill();
        _horizontal.Kill();
        transform.localPosition = new Vector3(0, 300, 0);
        _active = false;
    }
}

public enum BonusType
{
    Regeneration,
    Gold,
    Upgrade,
}