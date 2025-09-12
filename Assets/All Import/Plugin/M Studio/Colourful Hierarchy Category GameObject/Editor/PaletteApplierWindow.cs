using UnityEditor;
using UnityEngine;

namespace MStudio
{
    public class PaletteApplierWindow : EditorWindow
    {
        ColorPalette selectedPalette;

        [MenuItem("Tools/Hierarchy Style/Apply Palette...")]
        public static void ShowWindow()
        {
            var wnd = GetWindow<PaletteApplierWindow>("Palette Applier");
            wnd.minSize = new Vector2(360, 110);
            wnd.LoadCurrent();
        }

        void LoadCurrent()
        {
            selectedPalette = StyleHierarchy.GetActivePalette();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Apply Color Palette to Hierarchy", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            selectedPalette = (ColorPalette)EditorGUILayout.ObjectField("Palette", selectedPalette, typeof(ColorPalette), false);

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply (temporary)"))
            {
                StyleHierarchy.SetActivePaletteTemporary(selectedPalette);
            }

            if (GUILayout.Button("Apply and Save"))
            {
                StyleHierarchy.SetActivePalette(selectedPalette);
            }

            if (GUILayout.Button("Clear"))
            {
                StyleHierarchy.SetActivePalette(null);
                selectedPalette = null;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Apply (temporary) = só para visualizar agora (não salva). Apply and Save = salva localmente (EditorPrefs) — escolha do dev sem afetar o Git.", MessageType.Info);
        }
    }
}
