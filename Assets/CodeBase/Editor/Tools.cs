using Assets.CodeBase.Services;
using CodeBase.Infrastructure.Service;
using CodeBase.Progress;
using Scripts.LevelLogic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        [MenuItem("Tools/Save prefs")]
        public static void SavePrefs() 
        {
            AllServices.Container.Single<ISaveLoadService>().InformProgressWriters();
            PlayerPrefs.Save();
        }
        [MenuItem("Tools/Win")]
        public static void Win()
        {
            Object.FindAnyObjectByType<Level>().LevelCompleted();

        }
    }
}