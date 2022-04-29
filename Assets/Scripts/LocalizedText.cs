using UnityEngine;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour
{
    private const string English = "English";
    private const string Russian = "Russian";

    public static List<LocalizedText> LocalizedTexts = new List<LocalizedText>();

    [SerializeField] private string _ruText;
    [SerializeField] private string _enText;

    [SerializeField] private TMP_Text _text;

    [SerializeField] private SaveData _saveData;

    private void OnEnable()
    {
        LocalizedTexts.Add(this);
        ChangeText(_saveData.Language);
    }

    private void OnDisable()
    {
        LocalizedTexts.Remove(this);
    }

    private void ChangeText(string language)
    {
        if (language == English)
        {
            _text.text = _enText;
        }
        else if (language == Russian)
        {
            _text.text = _ruText;
        }
    }

    public static void ChangeLanguage(string language)
    {
        foreach (var localizedText in LocalizedTexts)
        {
            localizedText.ChangeText(language);
        }
    }
}
