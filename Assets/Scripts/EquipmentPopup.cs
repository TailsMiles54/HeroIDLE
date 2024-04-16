using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentPopup : Popup<EquipmentPopupSettings>
{
    [SerializeField] private RawImage _targetImage;
    private Camera _camera;
    private EquipmentController _equipmentController;

    public EquipmentElementController Weapons;
    public EquipmentElementController Shields;
    public EquipmentElementController Armors;
    public EquipmentElementController Accessories;
    public EquipmentElementController HeadArmors;
    public EquipmentElementController Hats;

    public override void Setup(EquipmentPopupSettings settings)
    {
        _camera = settings.CharacterCamera;
        _equipmentController = settings.EquipmentControllerPuppet;
        
        int height = 1024; // output texture height
        int width = 1024; // output texture height
        int depth = 24;	// output texture color depth

        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        _camera.targetTexture = renderTexture;
        
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Rect rect = new Rect(0, 0, width, height);
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

    public void ClosePopup()
    {
        PopupSystem.Instance.HidePopup();
    }
}

public class EquipmentPopupSettings : BasePopupSettings
{
    public Camera CharacterCamera;
    public EquipmentController EquipmentControllerPuppet;
}