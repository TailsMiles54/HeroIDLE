using System;
using UnityEngine;
using VInspector;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        SetupAnimator();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void SetupAnimator()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}