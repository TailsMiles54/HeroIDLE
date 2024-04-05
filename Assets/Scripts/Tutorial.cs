using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoSingleton<Tutorial>
{
    [SerializeField] private Image _attackTutorImage;
    
    [SerializeField] private Image _upgradeTutorImage;
    
    [SerializeField] private Image _nextEnemyTutorImage;
    [SerializeField] private Transform _nextEnemyTutorArrow;
    
    private void Start()
    {
        if (!SaveManager.Instance.LoadTutorialState())
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_attackTutorImage.DOFade(1, 1f));
            sequence.PrependInterval(2);
            sequence.Append(_attackTutorImage.DOFade(0, 0.2f));
        
            sequence.Append(_upgradeTutorImage.DOFade(1, 1f));
            sequence.PrependInterval(2);
            sequence.Append(_upgradeTutorImage.DOFade(0, 0.2f));

            Sequence arrowSequence = DOTween.Sequence();
            arrowSequence.Append(_nextEnemyTutorArrow.DOScale(1.4f, 1.4f));
            arrowSequence.Append(_nextEnemyTutorArrow.DOScale(1, 0.4f));
            arrowSequence.SetLoops(-1);
        
            sequence.Append(_nextEnemyTutorImage.DOFade(1, 1f));
            sequence.PrependInterval(2);
            sequence.Append(_nextEnemyTutorImage.DOFade(0, 0.2f));
        }
    }
}