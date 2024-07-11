using Lean.Localization;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageConfigurator : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    private void Awake()
    {
        List<string> optoins = new List<string>();
        foreach (Language optoin in Enum.GetValues(typeof(Language)))
        {
            optoins.Add(optoin.ToString());
        }
        dropdown.AddOptions(optoins);
    }
    private void OnEnable() =>
        dropdown.onValueChanged.AddListener(SetCurrentLanguageAll);
    private void OnDisable() => 
        dropdown.onValueChanged.RemoveListener(SetCurrentLanguageAll);
    public void SetCurrentLanguageAll(int value)
    {
        Language language =  (Language)value;
        LeanLocalization.SetCurrentLanguageAll(language.ToString());
    }
}
public enum Language
{
    English,
    Russian,
    Turkey,
    Italian,
    Spanish,
}
