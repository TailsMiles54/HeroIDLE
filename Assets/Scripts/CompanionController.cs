using System.Collections;
using UnityEngine;

public class CompanionController : Fighter
{
    [field: SerializeField] public CompanionSetting CompanionSetting { get; private set; }

    private Coroutine _autoAttack;

    public void Setup(CompanionSetting companionSetting)
    {
        CompanionSetting = companionSetting;
        _autoAttack = StartCoroutine(StartAutoAttack());
    }

    private IEnumerator StartAutoAttack()
    {
        while (CompanionSetting.AutoAttackTime < 15 && Health > 0 && CompanionSetting.AutoAttackTime > 0)
        {
            yield return new WaitForSeconds(CompanionSetting.AutoAttackTime);
            Attack();
        }
    }

    public void Attack()
    {
        var damage = CompanionSetting.Damage;

        AnimationController.Attack();
        BattleManager.Instance.DamagePlayer(damage);
    }
}