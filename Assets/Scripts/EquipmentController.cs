using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    private int _indexWeapons;
    private int _indexShields;
    private int _indexArmors;
    private int _indexAccessories;
    private int _indexHeadArmors;
    private int _indexHats;
    
    [field: SerializeField] public GameObject Hair { get; private set; }
    
    [field: SerializeField] public List<GameObject> Weapons { get; private set; }
    [field: SerializeField] public List<GameObject> Shields { get; private set; }
    [field: SerializeField] public List<GameObject> Armors { get; private set; }
    [field: SerializeField] public List<GameObject> Accessories { get; private set; }
    [field: SerializeField] public List<GameObject> HeadArmors { get; private set; }
    [field: SerializeField] public List<GameObject> Hats { get; private set; }

    public void ChangeEquipment(EquipmentType equipmentType, int index)
    {
        var equipmentGameObjects = new List<GameObject>();
        switch (equipmentType)
        {
            case EquipmentType.Weapon:
                equipmentGameObjects = Weapons;
                break;
            case EquipmentType.Shield:
                equipmentGameObjects = Shields;
                break;
            case EquipmentType.Armor:
                equipmentGameObjects = Armors;
                break;
            case EquipmentType.Accessory:
                equipmentGameObjects = Accessories;
                break;
            case EquipmentType.HeadArmor:
                equipmentGameObjects = HeadArmors;
                break;
            case EquipmentType.Hat:
                equipmentGameObjects = Hats;
                break;
        }

        foreach (var equipmentGameObject in equipmentGameObjects)
        {
            equipmentGameObject.SetActive(equipmentGameObjects.IndexOf(equipmentGameObject) == index);
        }
    } 
}

public enum EquipmentType
{
    Weapon,
    Shield,
    Armor,
    Accessory,
    HeadArmor,
    Hat
}