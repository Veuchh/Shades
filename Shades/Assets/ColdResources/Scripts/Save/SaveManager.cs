using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public SaveData Data;



    private void Awake()
    {
        Instance = this;
        LoadGame();
    }

    public void SaveGame()
    {
        Serializer.Save("save.dat", Data);
    }

    void LoadGame()
    {
        Data = Serializer.Load<SaveData>("save.dat");
        if (Data == null) Data = new SaveData();
    }
}
