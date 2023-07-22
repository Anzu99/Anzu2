#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SystemTab
{
    public SystemTab()
    {
        EditorManager.updateActions.Add(OnUpdate);
    }

    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(1)]
    private void Fix()
    {
        ANZU_Generate.GenerateSystemManager();
    }


    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(2)]
    private void Generate()
    {
        if (showCreateSystem)
        {
            ANZU_Generate.GenerateSystem(directory, listSystem);
            Fix();
        }
        else if (showCreateDirectory)
        {
            foreach (var item in listDirectory)
            {
                ANZU_Generate.GenerateSystem(item.directory, item.listSystem);
            }
            Fix();
        }
    }

    #region Create system in ECS/System
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(3)]
    private void CreateSystem()
    {
        showCreateSystem = !showCreateSystem;
    }

    bool showCreateSystem = false;
    [Title("Create System"), HideLabel]
    [ValidateInput("ConditionSystemName", "Not contains System", InfoMessageType.Warning), PropertyOrder(6)]
    [ShowIf("showCreateSystem")]
    public string SystemNameValue = "";
    private bool ConditionSystemName(string value)
    {
        bool res = !value.Contains("System") && !value.Contains("system") && !value.Contains(" ");
        return res;
    }

    [ShowIf("showCreateSystem")]
    [PropertySpace(25), HideLabel, BoxGroup("List System"), PropertyOrder(7)]
    public List<string> listSystem;

    #endregion

    #region Create system in multi directory
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(4)]
    private void CreateDrirector()
    {
        showCreateDirectory = !showCreateDirectory;
    }
    bool showCreateDirectory = false;
    [Title("Create Directory"), HideLabel]
    [ValidateInput("ConditionDirectoryName", "'/' in first", InfoMessageType.Warning), PropertyOrder(6)]
    [ShowIf("showCreateDirectory")]
    public string directory = "";
    private bool ConditionDirectoryName(string value)
    {
        char[] chars = value.ToCharArray();
        if (chars.Length == 0) return false;
        bool res = chars[0] == '/';
        return res;
    }
    [ShowIf("showCreateDirectory"), PropertyOrder(7)]
    public List<DirectoryAndComponent> listDirectory;


    #endregion

    private void OnUpdate()
    {
        if (Event.current.type == EventType.KeyDown)
        {
            if (Event.current.keyCode == KeyCode.Return)
            {
                if (showCreateSystem)
                {
                    AddSystem();
                    SystemNameValue = "";
                }
                if (showCreateDirectory)
                {
                    AddDirectory();
                }

            }
        }
    }

    private void AddSystem()
    {
        listSystem ??= new List<string>();
        if (SystemNameValue != "" && ConditionSystemName(SystemNameValue))
        {
            listSystem.Add(SystemNameValue);
        }
    }

    public void AddDirectory()
    {
        listDirectory ??= new List<DirectoryAndComponent>();
        if (directory != "" && ConditionDirectoryName(directory))
        {
            listDirectory.Add(new DirectoryAndComponent()
            {
                directory = directory,
                listSystem = new List<string>()
            });
        }
    }

    [Serializable]
    public class DirectoryAndComponent
    {
        public string directory = "";
        public List<string> listSystem;
    }
}
#endif