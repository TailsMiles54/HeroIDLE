using DG.Tweening;
using TMPro;
using UnityEngine;

public class Flying3DText : MonoBehaviour
{
    [SerializeField] private TMP_Text _tmp;
    
    public void Setup(Color32 color, string text)
    {
        _tmp.color = color;
        _tmp.text = text;
    }
    
    void Start()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0,-180,0);

        _tmp.transform.DOMoveY(4, 2).onComplete += () =>
        {
            Destroy(gameObject);
        };
    }
}
