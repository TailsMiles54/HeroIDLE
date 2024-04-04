using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using YG;

public class SaveManager : MonoSingleton<SaveManager>
{
    public event Action Loaded;
    
    public void Save(PlayerController playerController)
    {
    }

    public void RestoreSave()
    {
    }
    
    public void GetLoad()
    {
    }
}
