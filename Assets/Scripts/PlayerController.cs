using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlackTailsUnityTools.Editor;
using UnityEngine;

public class PlayerController : Fighter
{
    public static PlayerController Instance { get; private set; }
    public int Money {get; private set;}
    public int Score {get; private set;}
    
    private UpgradesSettings UpgradesSettings => SettingsProvider.Get<UpgradesSettings>();
    
    private List<UpgradeLevel> _upgrades = new List<UpgradeLevel>()
    {
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.Damage, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.Health, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.AutoAttackSpeed, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.MoneyBonus, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.CriticalDamage, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.CriticalChance, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.Heal, Level = 0 },
    };
    
    public float MaxHealth => UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.Health, _upgrades.First(x => x.Type == UpgradeSetting.UpgradeType.Health).Level);

    private Coroutine _autoAttackCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Health = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.Health, _upgrades.First(x => x.Type == UpgradeSetting.UpgradeType.Health).Level);
    }

    public void Upgrade(UpgradeSetting.UpgradeType upgradeType)
    {
        _upgrades.First(x => x.Type == upgradeType).Level++;

        if (upgradeType == UpgradeSetting.UpgradeType.AutoAttackSpeed)
        {
            if (_autoAttackCoroutine != null) 
                StopCoroutine(_autoAttackCoroutine);
                    
            _autoAttackCoroutine = StartCoroutine(StartAutoAttack());
        }
    }

    private IEnumerator StartAutoAttack()
    {
        while (true)
        {
            var waitTime = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.AutoAttackSpeed,
                _upgrades.First(x => x.Type == UpgradeSetting.UpgradeType.AutoAttackSpeed).Level);
            Debug.Log(waitTime);
            yield return new WaitForSeconds(waitTime);
            Attack();
        }
    }

    public void Attack()
    {
        var damage = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.Damage, _upgrades
                         .First(x => x.Type == UpgradeSetting.UpgradeType.Damage).Level);

        AnimationController.Attack();
        BattleManager.Instance.DamageEnemy(damage);
    }

    public void AddReward(int money, int score)
    {
        Money += money;
        Score += score;
    }

    public class UpgradeLevel
    {
        public UpgradeSetting.UpgradeType Type;
        public int Level;
    }
}