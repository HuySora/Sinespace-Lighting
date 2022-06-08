using ProjectName.Extension;

namespace ProjectName.EditorTools
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.SceneManagement;
    using System.Collections.Generic;

    public class GameObjectList : EditorWindow
    {
        [MenuItem("Tools/GameObjectList")]
        public static void ShowWindow()
        {
            var window = GetWindow<GameObjectList>();
            window.titleContent = new GUIContent("GameObjectList");
            window.GatherGameObjects();
        }

        private static readonly GUILayoutOption s_ButtonWidth = GUILayout.Width(80);
        private static readonly GUILayoutOption s_BigButtonHeight = GUILayout.Height(35);

        private List<GameObject> m_GameObjects = new List<GameObject>();
        private Vector2 m_ScrollPos;
        
        private void OnGUI()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Refresh", s_BigButtonHeight))
                {
                    GatherGameObjects();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);

            EditorGUILayout.LabelField("Hidden Objects (" + m_GameObjects.Count + ")", EditorStyles.boldLabel);
            m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);
            for (int i = 0; i < m_GameObjects.Count; i++)
            {
                var hiddenObject = m_GameObjects[i];
                GUILayout.BeginHorizontal();
                {
                    if (hiddenObject == null)
                    {
                        GUILayout.Label("null");
                        GUILayout.FlexibleSpace();
                        GUILayout.Box("Select", s_ButtonWidth);
                        GUILayout.Box("Reveal", s_ButtonWidth);
                        GUILayout.Box("Delete", s_ButtonWidth);
                    }
                    else
                    {

                        GUILayout.Label(hiddenObject.name);
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("Select", s_ButtonWidth))
                        {
                            Selection.activeGameObject = hiddenObject;
                        }

                        string label;
                        var defaultColor = GUI.backgroundColor;
                        var options = new GUILayoutOption[] { s_ButtonWidth };

                        if (hiddenObject.IsHidden())
                        {
                            label = "Revealed";
                            GUI.backgroundColor = Color.red;
                        }
                        else
                        {
                            label = "Hidden";
                            GUI.backgroundColor = Color.green;
                        }

                        if (GUILayout.Button(label, options))
                        {
                            hiddenObject.hideFlags ^= HideFlags.HideInHierarchy;
                            EditorSceneManager.MarkSceneDirty(hiddenObject.scene);
                        }

                        GUI.backgroundColor = defaultColor;
                        
                        if (GUILayout.Button("Delete", s_ButtonWidth))
                        {
                            var scene = hiddenObject.scene;
                            DestroyImmediate(hiddenObject);
                            EditorSceneManager.MarkSceneDirty(scene);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private void GatherGameObjects()
        {
            m_GameObjects.Clear();
            m_GameObjects.AddRange(FindObjectsOfType<GameObject>());
            Repaint();
        }
    }
}