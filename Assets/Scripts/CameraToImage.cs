using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraToImage : MonoBehaviour
{
    [SerializeField] private RawImage _targetImage;
    [SerializeField] private GameObject _localPlayer;
    [SerializeField] private Camera _camera;
    
    private void Start()
    {
        int height = 1024; // output texture height
        int width = 1024; // output texture height
        int depth = 24;	// output texture color depth

        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        _camera.targetTexture = renderTexture;
        
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Rect rect=new Rect(0, 0, width, height);
        StartCoroutine(CheckCameraTexture());
    }

    private IEnumerator CheckCameraTexture()
    {
        while (_camera.activeTexture == null)
        {
            yield return new WaitForEndOfFrame();
        }
        _targetImage.texture = _camera.activeTexture;
    }
}
