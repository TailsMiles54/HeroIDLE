using System;
using DG.Tweening;
using UnityEngine;

public class LandedProgressBar : MonoBehaviour
{
    [SerializeField] private Axis _axis;
    [SerializeField] private Transform _fillParent;

    public void UpdateProgress(float progress)
    {

        switch (_axis)
        {
            case Axis.X:
                var poxX = Remap(progress,0f, 1f, -0.8f, 1f);
                _fillParent.DOLocalMoveX(poxX, 0.2f);
                break;
            case Axis.Y:
                var posY = Remap(progress,0f, 1f, -0.997f, 0.85f);
                _fillParent.DOLocalMoveY(posY, 0.2f);
                break;
        }
    }

    public void ToMinWithTime(float time)
    {
        switch (_axis)
        {
            case Axis.X:
                _fillParent.DOLocalMoveX(1f, 0.05f).OnComplete(() =>
                {
                    _fillParent.DOLocalMoveX(-0.8f, time-0.05f).SetEase(Ease.Linear);
                });
                break;
            case Axis.Y:
                _fillParent.DOLocalMoveY(-0.997f, 0.05f).OnComplete(() =>
                {
                    _fillParent.DOLocalMoveY(0.85f, time-0.05f).SetEase(Ease.Linear);
                });
                break;
        }
    }
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

public enum Axis
{
    X,
    Y,
}