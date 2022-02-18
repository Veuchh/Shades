using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSave : MonoBehaviour
{
    SaveData Data;
    [SerializeField] InputField field;

    private void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        Data.characterName = field.text;
        Debug.Log(Data.characterName);
        Serializer.Save("debugSave.dat", Data);
    }

    public void LoadGame()
    {
        Data = Serializer.Load<SaveData>("debugSave.dat");
        if (Data == null) Data = new SaveData();
        Debug.Log(Data.characterName);
    }
}
