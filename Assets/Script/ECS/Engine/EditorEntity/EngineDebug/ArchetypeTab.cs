// using System;
// using System.Collections.Generic;
// using AV;
// #if UNITY_EDITOR
// using Sirenix.OdinInspector.Editor;
// using Sirenix.Utilities;
// using Sirenix.Utilities.Editor;
// using UnityEditor;
// using UnityEngine;

// public class ArchetypeDebug
// {
// }
// public class ArchetypeDebugDrawer : OdinValueDrawer<ArchetypeDebug>
// {
//     protected override void DrawPropertyLayout(GUIContent label)
//     {
//         if (!Application.isPlaying) return;
//         ArchetypeManager archetypeManager = World.archetypeManager;
//         if (archetypeManager == null) return;

//         GUILayout.Space(10);
//         for (ushort i = 0; i < archetypeManager.count; i++)
//         {
//             string name = World.GetEntity(archetypeManager.listArchetypes[i].entities[0]).Name;

//             SirenixEditorGUI.BeginLegendBox();
//             GUILayout.BeginHorizontal();

//             GUILayout.Label("   Count: " + archetypeManager.listArchetypes[i].count, SirenixGUIStyles.LeftAlignedGreyLabel);

//             if (GUILayout.Button(name))
//             {
//                 ArchetypeItem.archetypeID = i;
//                 EditorWindow.GetWindow<ArchetypeItem>().Show();
//             }
//             GUILayout.EndHorizontal();
//             SirenixEditorGUI.EndLegendBox();
//             GUILayout.Space(10);
//         }
//         this.CallNextDrawer(label);
//     }


// }


// public class ArchetypeItem : OdinEditorWindow
// {
//     public static ushort archetypeID;
//     bool foldout = false;
//     List<string> names;

//     Color colorListNames;
//     Color colorListEntity;

//     protected override void Initialize()
//     {
//         names = new List<string>();
//         colorListNames = Color.HSVToRGB(1, 1, 1);
//         colorListEntity = Color.HSVToRGB(0.35f, 1, 0.6f);
//     }


//     protected override void OnGUI()
//     {
//         ArchetypeManager archetypeManager = World.archetypeManager;
//         if (archetypeManager == null) return;
//         DrawListNameArchetype(archetypeManager);
//         GUILayout.Space(15);
//         DrawListEntity(archetypeManager);
//         GUIHelper.RequestRepaint();
//     }
//     public void GetListName(ArchetypeManager archetypeManager)
//     {
//         names.Clear();
//         // Component compFlag = archetypeManager.archetypes[archetypeID].flag;
//         Component flagTmp = Component.None;

//         for (var i = 1; i < Enum.GetNames(typeof(Component)).Length; i++)
//         {
//             flagTmp = AV_ENUM_EXTENSION.Next(flagTmp);
//             // if ((flagTmp & compFlag) != 0)
//             // {
//             //     names.Add(flagTmp.ToString());
//             // }
//         }
//     }

//     public void DrawListNameArchetype(ArchetypeManager archetypeManager)
//     {
//         GUIHelper.PushColor(colorListNames);
//         SirenixEditorGUI.BeginBox();
//         SirenixEditorGUI.BeginBoxHeader();
//         GUIHelper.PopColor();

//         foldout = SirenixEditorGUI.Foldout(foldout, "Archetype");
//         GetListName(archetypeManager);
//         SirenixEditorGUI.EndBoxHeader();

//         if (foldout)
//         {
//             for (var i = 0; i < names.Count; i++)
//             {
//                 SirenixEditorGUI.BeginVerticalList(true, false);
//                 GUILayout.Label(names[i], SirenixGUIStyles.LabelCentered);
//                 GUILayout.Space(5);
//                 SirenixEditorGUI.EndVerticalList();
//             }
//         }

//         SirenixEditorGUI.EndBox();
//     }


//     private const float TileSize = 30;
//     public void DrawListEntity(ArchetypeManager archetypeManager)
//     {
//         SirenixEditorGUI.BeginBoxHeader();
//         GUILayout.Label("Count: ", SirenixGUIStyles.LabelCentered);
//         SirenixEditorGUI.EndBoxHeader();

//         ushort[] id = archetypeManager.listArchetypes[archetypeID].entities;

//         float width = position.width;
//         width -= 30;

//         Rect rect = GUILayoutUtility.GetLastRect();
//         rect.width = width;
//         rect.x += 15;
//         rect.y += 20;

//         int x = (int)(width / 20);

//         for (int i = 0; i < x * x; i++)
//         {
//             Rect tileRect = RectExtensions.SplitGrid(rect, TileSize, TileSize, i);

//             SirenixEditorGUI.DrawBorders(tileRect.SetWidth(tileRect.width + 1).SetHeight(tileRect.height + 1), 1);
//             SirenixEditorGUI.DrawSolidRect(new Rect(tileRect.x + 1, tileRect.y + 1, tileRect.width - 1, tileRect.height - 1), new Color(0f, 1f, 0f, 0.3f));

//             if (i < id.Length && id[i] != 0)
//             {
//                 GUIHelper.PushColor(Color.red);
//                 GUI.Label(tileRect.AlignCenter(25).AlignMiddle(25), id[i].ToString());
//                 GUIHelper.PopColor();
//             }
//             if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
//             {
//                 Debug.LogError(i);
//                 Event.current.Use();
//             }


//         }


//     }

// }
// #endif