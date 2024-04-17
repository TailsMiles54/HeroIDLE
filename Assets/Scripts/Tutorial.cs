using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Tutorial : MonoSingleton<Tutorial>
{
    [SerializeField] private Image _attackTutorImage;
    [SerializeField] private TMP_Text _attackTutorText;
    
    [SerializeField] private Image _upgradeTutorImage;
    [SerializeField] private TMP_Text _upgradeTutorText;
    
    [SerializeField] private Image _nextEnemyTutorImage;
    [SerializeField] private TMP_Text _nextEnemyTutorText;
    
    [SerializeField] private Image _nextEnemyTutorArrowImage;
    [SerializeField] private Transform _nextEnemyTutorArrow;
    
    public void Start()
    {
#if PLATFORM_WEBGL
        if (!YandexGame.savesData.TutorialComplete)
#else
        if(!SaveManager.Instance.PlayerSave.TutorialComplete)
#endif
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_attackTutorImage.DOFade(1, 1f));
            sequence.Join(_attackTutorText.DOFade(1, 1f));
            sequence.Append(_attackTutorImage.transform.DOScale(1.2f, 1f));
            sequence.Append(_attackTutorImage.DOFade(0, 0.6f));
            sequence.Join(_attackTutorText.DOFade(0, 0.6f));
        
            sequence.Append(_upgradeTutorImage.DOFade(1, 1f));
            sequence.Join(_upgradeTutorText.DOFade(1, 1f));
            sequence.Append(_upgradeTutorImage.transform.DOScale(1.2f, 1f));
            sequence.Append(_upgradeTutorImage.DOFade(0, 0.6f));
            sequence.Join(_upgradeTutorText.DOFade(0, 0.6f));

            Sequence arrowSequence = DOTween.Sequence();
            arrowSequence.SetLoops(3);
        
            sequence.Append(_nextEnemyTutorImage.DOFade(1, 1f));
            sequence.Join(_nextEnemyTutorText.DOFade(1, 1f));
            sequence.Join(_nextEnemyTutorArrowImage.DOFade(1f, 1.4f));
            
            sequence.Append(_nextEnemyTutorImage.transform.DOScale(1.2f, 1f));
            
            sequence.Join(arrowSequence.Append(_nextEnemyTutorArrow.DOScale(1.4f, 1.4f)));
            sequence.Join(arrowSequence.Append(_nextEnemyTutorArrow.DOScale(1, 0.8f)));
            
            sequence.Append(_nextEnemyTutorImage.DOFade(0, 0.6f));
            sequence.Join(_nextEnemyTutorText.DOFade(0, 0.6f));
            sequence.Join(_nextEnemyTutorArrowImage.DOFade(0, 0.6f));

            sequence.onComplete += () =>
            {
                SaveManager.Instance.TutorialComplete();
            };
        }
    }
}