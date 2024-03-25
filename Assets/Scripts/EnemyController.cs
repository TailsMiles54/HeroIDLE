using System.Collections;
using UnityEngine;

public class EnemyController : Fighter
{
    [field: SerializeField] public EnemySetting EnemySetting { get; private set; }

    private Coroutine _autoAttack;

    public void Setup(EnemySetting enemySetting)
    {
        EnemySetting = enemySetting; 
        Health = enemySetting.Health;
        _autoAttack = StartCoroutine(StartAutoAttack());
    }

    private IEnumerator StartAutoAttack()
    {
        while (EnemySetting.AutoAttackTime < 15 && Health > 0 && EnemySetting.AutoAttackTime > 1)
        {
            yield return new WaitForSeconds(EnemySetting.AutoAttackTime);
            Attack();
        }
    }

    public void Attack()
    {
        var damage = EnemySetting.Damage;

        AnimationController.Attack();
        BattleManager.Instance.DamagePlayer(damage);
    }
}