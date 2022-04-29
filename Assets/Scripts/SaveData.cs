using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public string Language = "English";

    private void Awake()
    {
        Load();
    }

    public void ChangeLanguage(string language)
    {
        Language = language;
        LocalizedText.ChangeLanguage(language);
        Save();
    }

    [ContextMenu("Save")]
    public void Save()
    {
        File.WriteAllText(Application.persistentDataPath + "/Save.json", JsonUtility.ToJson(this));
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.json"))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.persistentDataPath + "/Save.json"), this);
        }
        else
        {
            File.Create(Application.persistentDataPath + "/Save.json");
            Save();
        }
    }
}
