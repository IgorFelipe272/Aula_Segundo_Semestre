/*
using UnityEditor;
using UnityEngine;

namespace MStudio
{
    [InitializeOnLoad]
    public class StyleHierarchy
    {
        static string[] dataArray;//Find ColorPalette GUID
        static string path;//Get ColorPalette(ScriptableObject) path
        static ColorPalette colorPalette;

        static StyleHierarchy()
        {
            dataArray = AssetDatabase.FindAssets("t:ColorPalette");

            if (dataArray.Length >= 1)
            {    //We have only one color palette, so we use dataArray[0] to get the path of the file
                path = AssetDatabase.GUIDToAssetPath(dataArray[0]);

                colorPalette = AssetDatabase.LoadAssetAtPath<ColorPalette>(path);

                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
            }
        }

        private static void OnHierarchyWindow(int instanceID, Rect selectionRect)
        {
            //To make sure there is no error on the first time the tool imported in project
            if (dataArray.Length == 0) return;

            UnityEngine.Object instance = EditorUtility.InstanceIDToObject(instanceID);

            if (instance != null)
            {
                for (int i = 0; i < colorPalette.colorDesigns.Count; i++)
                {
                    var design = colorPalette.colorDesigns[i];

                    //Check if the name of each gameObject is begin with keyChar in colorDesigns list.
                    if (instance.name.StartsWith(design.keyChar))
                    {
                        //Remove the symbol(keyChar) from the name.
                        string newName = instance.name.Substring(design.keyChar.Length);
                        //Draw a rectangle as a background, and set the color.
                        EditorGUI.DrawRect(selectionRect, design.backgroundColor);

                        //Create a new GUIStyle to match the desing in colorDesigns list.
                        GUIStyle newStyle = new GUIStyle
                        {
                            alignment = design.textAlignment,
                            fontStyle = design.fontStyle,
                            normal = new GUIStyleState()
                            {
                                textColor = design.textColor,
                            }
                        };

                        //Draw a label to show the name in upper letters and newStyle.
                        //If you don't like all capital latter, you can remove ".ToUpper()".
                        EditorGUI.LabelField(selectionRect, newName.ToUpper(), newStyle);
                    }
                }
            }
        }
    }
}


*/

using UnityEditor;
using UnityEngine;

namespace MStudio
{
    [InitializeOnLoad]
    public static class StyleHierarchy
    {
        private const string PREF_KEY = "MStudio.ActivePalettePath";
        private static ColorPalette activePalette;

        static StyleHierarchy()
        {
            LoadActivePalette();
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindow;
        }

        private static void LoadActivePalette()
        {
            string path = EditorPrefs.GetString(PREF_KEY, "");
            if (!string.IsNullOrEmpty(path))
            {
                activePalette = AssetDatabase.LoadAssetAtPath<ColorPalette>(path);
            }

            // fallback: se não achar nada no pref, pega o primeiro ColorPalette do projeto
            if (activePalette == null)
            {
                string[] guids = AssetDatabase.FindAssets("t:ColorPalette");
                if (guids.Length > 0)
                {
                    path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    activePalette = AssetDatabase.LoadAssetAtPath<ColorPalette>(path);
                    if (!string.IsNullOrEmpty(path))
                        EditorPrefs.SetString(PREF_KEY, path);
                }
            }
        }

        private static void OnHierarchyWindow(int instanceID, Rect selectionRect)
        {
            if (activePalette == null) return;

            UnityEngine.Object instance = EditorUtility.InstanceIDToObject(instanceID);
            if (instance == null) return;

            foreach (var design in activePalette.colorDesigns)
            {
                if (string.IsNullOrEmpty(design.keyChar)) continue;
                if (instance.name.StartsWith(design.keyChar))
                {
                    string newName = instance.name.Substring(design.keyChar.Length);

                    EditorGUI.DrawRect(selectionRect, design.backgroundColor);

                    GUIStyle newStyle = new GUIStyle
                    {
                        alignment = design.textAlignment,
                        fontStyle = design.fontStyle,
                        normal = new GUIStyleState()
                        {
                            textColor = design.textColor
                        }
                    };

                    EditorGUI.LabelField(selectionRect, newName.ToUpper(), newStyle);
                    return; // aplica a primeira correspondência encontrada
                }
            }
        }

        // Métodos públicos para aplicar/get a paleta (podem ser chamados por outros scripts/editor windows)
        public static void SetActivePalette(ColorPalette palette)
        {
            if (palette == null)
            {
                EditorPrefs.DeleteKey(PREF_KEY);
                activePalette = null;
            }
            else
            {
                string path = AssetDatabase.GetAssetPath(palette);
                if (!string.IsNullOrEmpty(path))
                {
                    EditorPrefs.SetString(PREF_KEY, path);
                    activePalette = palette;
                }
            }
            EditorApplication.RepaintHierarchyWindow();
        }

        // Aplica temporariamente (sem gravar no EditorPrefs)
        public static void SetActivePaletteTemporary(ColorPalette palette)
        {
            activePalette = palette;
            EditorApplication.RepaintHierarchyWindow();
        }

        public static ColorPalette GetActivePalette()
        {
            return activePalette;
        }
    }
}
