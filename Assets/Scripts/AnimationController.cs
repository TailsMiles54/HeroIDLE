using UnityEngine;
using VInspector;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    [Button("SetupAnimator")]
    public void SetupAnimator()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}