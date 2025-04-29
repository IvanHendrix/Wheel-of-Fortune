using UnityEditor;
using UnityEngine;
using System.IO;

namespace Editor
{
    public class ProgressResetTool : EditorWindow
    {
        private static string SavePath => Application.persistentDataPath + "/PlayerProgress.json";

        [MenuItem("Tools/Reset Player Progress")]
        public static void ShowWindow()
        {
            GetWindow<ProgressResetTool>("Reset Progress");
        }

        private void OnGUI()
        {
            GUILayout.Label("Reset Player Progress", EditorStyles.boldLabel);

            if (GUILayout.Button("Reset Progress"))
            {
                if (File.Exists(SavePath))
                {
                    File.Delete(SavePath);
                    Debug.Log("Player progress has been reset.");
                }
                else
                {
                    Debug.Log("No progress file found.");
                }
            }
        }
    }
}