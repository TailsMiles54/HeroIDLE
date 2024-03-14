using UnityEngine;

[CreateAssetMenu(menuName = "HeioIDLE/Settings/EnemiesSettings", fileName = "EnemiesSettings")]
public class EnemiesSettings : ScriptableObject
{
    [field: SerializeField] public EnemySetting EnemiesSettingsList { get; private set; }
}