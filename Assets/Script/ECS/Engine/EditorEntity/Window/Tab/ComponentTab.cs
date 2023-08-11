#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class ComponentTab
{
    public ComponentTab()
    {
        EditorManager.updateActions.Add(OnUpdate);
    }
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(0)]
    private void OpenComponentConfig()
    {
        ConfigComponentWindow configComponentWindow = EditorWindow.GetWindow<ConfigComponentWindow>();
        configComponentWindow.Initialize(this);
        configComponentWindow.Show();
    }

    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(1)]
    public void Fix()
    {
        ComponentEditorData componentEditorData = new ComponentEditorData();
        componentEditorData.LoadData();
        componentEditorData.FixComponentConfigData();

        ANZU_Generate.GenerateEntityEditor();
        ANZU_Generate.GenerateEntityPrefab();
    }


    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(2)]
    private void Generate()
    {
        if (showCreateComponent)
        {
            ANZU_Generate.GenerateComponentData(directory, listComponent);
        }
        else if (showCreateDirectory)
        {
            foreach (var item in listDirectory)
            {
                ANZU_Generate.GenerateComponentData(item.directory, item.listComponent);
            }
        }
        Fix();
    }

    #region Create component in ECS/Component
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(3)]
    private void CreateComponent()
    {
        showCreateComponent = !showCreateComponent;
    }

    bool showCreateComponent = false;
    [Title("Create Component"), HideLabel]
    [ValidateInput("conditionComponentName", "Not contains Component", InfoMessageType.Warning), PropertyOrder(60)]
    [ShowIf("showCreateComponent")]
    public string componentNameValue = "";
    private bool conditionComponentName(string value)
    {
        bool res = !value.Contains("Component") && !value.Contains("component") && !value.Contains(" ");
        return res;
    }

    [ShowIf("showCreateComponent")]
    [PropertySpace(25), HideLabel, BoxGroup("List Component"), PropertyOrder(70)]
    public List<string> listComponent;

    #endregion

    #region Create component in multi directory
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(4)]
    private void CreateDrirector()
    {
        showCreateDirectory = !showCreateDirectory;
    }
    bool showCreateDirectory = false;
    [Title("Create Directory"), HideLabel]
    [ValidateInput("conditionDirectoryName", "'/' in first", InfoMessageType.Warning), PropertyOrder(60)]
    [ShowIf("showCreateDirectory")]
    public string directory = "";
    private bool conditionDirectoryName(string value)
    {
        char[] chars = value.ToCharArray();
        if (chars.Length == 0) return false;
        bool res = chars[0] == '/';
        return res;
    }
    [ShowIf("showCreateDirectory"), PropertyOrder(70)]
    public List<DirectoryAndComponent> listDirectory;


    #endregion

    #region Remove Component

    [ShowIf("showRemoveComponent"), PropertyOrder(70), Title("Remove Component"), HideLabel]
    public Component componentRemove;
    [ShowIf("showRemoveComponent"), PropertyOrder(71), Title("List Component"), HideLabel]
    public List<Component> listComponentForRemove;
    private bool showRemoveComponent = false;
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(4)]
    private void RemoveComponent()
    {
        showRemoveComponent = !showRemoveComponent;
    }

    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(80)]
    [ShowIf("showRemoveComponent")]
    private void Remove()
    {
        string[] fileNames = new string[listComponentForRemove.Count];
        for (var i = 0; i < fileNames.Length; i++)
        {
            fileNames[i] = listComponentForRemove[i].ToString();
        }
        ANZU_Generate.RemoveFile(fileNames);
        Fix();
    }

    #endregion

    private void OnUpdate()
    {
        if (Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.Return)
            {
                if (showCreateComponent)
                {
                    AddComponent();
                    componentNameValue = "";
                }
                else if (showCreateDirectory)
                {
                    AddDirectory();
                }
                else if (showRemoveComponent)
                {
                    AddComponentForRemove();
                }

            }
        }
    }

    private void AddComponent()
    {
        listComponent ??= new List<string>();
        if (componentNameValue != "" && conditionComponentName(componentNameValue))
        {
            listComponent.Add(componentNameValue);
        }
    }

    public void AddDirectory()
    {
        listDirectory ??= new List<DirectoryAndComponent>();
        if (directory != "" && conditionComponentName(directory))
        {
            listDirectory.Add(new DirectoryAndComponent()
            {
                directory = directory,
                listComponent = new List<string>()
            });
        }
    }

    private void AddComponentForRemove()
    {
        listComponentForRemove ??= new List<Component>();
        if (componentRemove != Component.None)
        {
            listComponentForRemove.Add(componentRemove);
        }
    }

    [Serializable]
    public class DirectoryAndComponent
    {
        public string directory = "";
        public List<string> listComponent;
    }
}
#endif