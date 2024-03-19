using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}