using System.Collections;
using UnityEngine;

public class EnemyController : Fighter
{
    [field: SerializeField] public EnemySetting EnemySetting { get; private set; }

    public void Setup(EnemySetting enemySetting)
    {
        EnemySetting = enemySetting; 
        Health = enemySetting.Health;
        StartCoroutine(StartAutoAttack());
    }

    private IEnumerator StartAutoAttack()
    {
        while (true)
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

    ~EnemyController()
    {
        StopCoroutine(StartAutoAttack());
    }
}