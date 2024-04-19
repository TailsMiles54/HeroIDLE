using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlackTailsUnityTools.Editor;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerController : Fighter
{
    [field: SerializeField] public PlayerInfoPanel PlayerInfoPanel;
    [field: SerializeField] public Camera CharacterCamera {get; private set;}
    public static PlayerController Instance { get; private set; }
    public int Money {get; private set;}
    public int Score {get; private set;}
    public event Action<UpgradeSetting.UpgradeType> Upgraded;
    public EquipmentController EquipmentControllerPuppet;
    private UpgradesSettings UpgradesSettings => SettingsProvider.Get<UpgradesSettings>();
    
    public List<UpgradeLevel> Upgrades { get; private set; } = new List<UpgradeLevel>()
    {
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.ClickDamage, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.Damage, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.Health, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.AutoAttackSpeed, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.MoneyBonus, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.CriticalDamage, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.CriticalChance, Level = 0 },
        new UpgradeLevel() { Type = UpgradeSetting.UpgradeType.Heal, Level = 0 },
    };

    public List<Quest> Quests { get; private set; } = new List<Quest>()
    {
        new Quest("Test 1", "Test 1Test 1Test 1Test 1Test 1Test 1Test 1Test 1Test 1Test 1Test 1Test 1Test 1", QuestType.Hunt),
        new Quest("Test 2", "Test 2Test 2Test 2Test 2Test 2Test 2Test 2Test 2Test 2Test 2Test 2Test 2Test 2",QuestType.Upgrade),
    };
    
    public float MaxHealth => UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.Health, Upgrades.First(x => x.Type == UpgradeSetting.UpgradeType.Health).Level);

    private Coroutine _autoAttackCoroutine;
    private Coroutine _healCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    public float GetUpgradeValue(UpgradeSetting.UpgradeType upgradeType) =>
        UpgradesSettings.GetBonusValue(upgradeType, Upgrades.First(x => x.Type == upgradeType).Level);
    
    private void Start()
    {
        Health = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.Health, Upgrades.First(x => x.Type == UpgradeSetting.UpgradeType.Health).Level);
                    
        _autoAttackCoroutine = StartCoroutine(StartAutoAttack());
        _healCoroutine = StartCoroutine(Regeneration());
        
        SaveManager.Instance.GetLoad();
    }

    public void Upgrade(UpgradeSetting.UpgradeType upgradeType)
    {
        Upgrades.First(x => x.Type == upgradeType).Level++;

        if (upgradeType == UpgradeSetting.UpgradeType.AutoAttackSpeed)
        {
            if (_autoAttackCoroutine != null) 
                StopCoroutine(_autoAttackCoroutine);
                    
            _autoAttackCoroutine = StartCoroutine(StartAutoAttack());
        }
        
        if (upgradeType == UpgradeSetting.UpgradeType.Heal)
        {
            if (_healCoroutine != null) 
                StopCoroutine(_healCoroutine);
                    
            _healCoroutine = StartCoroutine(Regeneration());
        }

        var upgrade = JsonConvert.SerializeObject(Upgrades.First(x => x.Type == upgradeType));
        
        AppMetrica.Instance.ReportEvent("Upgrade", upgrade);
        
        Upgraded?.Invoke(upgradeType);
        PlayerInfoPanel.UpdatePanel(Instance);
        SaveManager.Instance.Save(Instance);
    }

    public void Review()
    {
        Health = MaxHealth;
        PlayerInfoPanel.UpdatePanel(Instance);
    }

    public void EquipmentPopup()
    {
        PopupSystem.Instance.ShowPopup(new EquipmentPopupSettings()
        {
            CharacterCamera = CharacterCamera,
            EquipmentControllerPuppet = EquipmentControllerPuppet
        });
    }
    
    public bool TryPurchase(int cost)
    {
        if (Money >= cost)
        {
            Money -= cost;
            return true;
        }

        return false;
    }

    private IEnumerator StartAutoAttack()
    {
        while (true)
        {
            var waitTime = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.AutoAttackSpeed,
                Upgrades.First(x => x.Type == UpgradeSetting.UpgradeType.AutoAttackSpeed).Level);
            yield return new WaitForSeconds(waitTime);
            Attack();
        }
    }

    private IEnumerator Regeneration()
    {
        while (true)
        {
            var heal = GetUpgradeValue(UpgradeSetting.UpgradeType.Heal);
            yield return new WaitForSeconds(5);
            Regeneration(heal);
            FlyingTextController.Instance.ShowText(Color.green, transform.parent, $"+{heal}");
            PlayerInfoPanel.UpdatePanel(Instance);
        }
    }

    public void Regeneration(float heal)
    {
        Health += heal;
    }

    public void ClickAttack()
    {
        var damage = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.ClickDamage, Upgrades
            .First(x => x.Type == UpgradeSetting.UpgradeType.ClickDamage).Level);

        AnimationController.Attack();
        BattleManager.Instance.DamageEnemy(damage);
    }

    public void Attack()
    {
        var damage = UpgradesSettings.GetBonusValue(UpgradeSetting.UpgradeType.Damage, Upgrades
                         .First(x => x.Type == UpgradeSetting.UpgradeType.Damage).Level);

        AnimationController.Attack();
        BattleManager.Instance.DamageEnemy(damage);
    }

    public void AddReward(int money, int score)
    {
        Money += money;
        Score += score;
    }

    [Serializable]
    public class UpgradeLevel
    {
        public UpgradeSetting.UpgradeType Type;
        public int Level;
    }

    public void LoadProgress(int savesDataScore, int savesDataMoney, List<UpgradeLevel> savesDataUpgrades)
    {
        if(savesDataScore != 0)
            Score = savesDataScore;
        
        if(savesDataMoney != 0)
            Money = savesDataMoney;
        
        if(savesDataUpgrades != null && savesDataUpgrades.Count != 0)
            Upgrades = savesDataUpgrades;
    }
}