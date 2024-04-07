using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTextController : MonoSingleton<FlyingTextController>
{
    [SerializeField] private Flying3DText _textPrefab;
    
    public void ShowText(Color32 color, Transform parent, string text)
    {
        var text3D = Instantiate(_textPrefab, parent);
        text3D.Setup(color, text);
    }
}
