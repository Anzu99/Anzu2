using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Collections.Generic;

public class ANZU_Generate
{
    public static void GenerateEntityEditor()
    {
        ComponentEditorData componentEditorData = new ComponentEditorData();
        componentEditorData.LoadData();
        string[] strs = new string[componentEditorData.componentConfigData.componentConfigItems.Count];
        for (var i = 0; i < strs.Length; i++)
        {
            strs[i] = componentEditorData.componentConfigData.componentConfigItems[i].component.ToString();
        }
        string[] componentNames = strs;
        
        // string filePath = $"Assets/Script/ECS/Component";
        // string[] componentNames = DirectoryUtility.GetAllFileInFolderAndChild(filePath, "cs");


        string module1 = $@"";
        string module2 = $@"";
        string module3 = $@"";

        string content3 = $@"";

        int count = 0;
        foreach (var item in componentNames)
        {
            string isShow_NAME = $"isShow{item}";
            string isShowIf = $"\"isShow{item}\"";

            string content1 = $@"
    [ColoredFoldoutGroup(Component.{item}, 0, 1, 0, 1), HideLabel, PropertyOrder({/* count++ */componentEditorData.componentConfigData.componentConfigItems[count++].order})]
    [ShowIf({isShowIf}, true), ShowInInspector]
    public {item} {item}
    {{
        get => {isShow_NAME} ? entity.GetComponent<{item}>(Component.{item}) : default;
        set => entity.GetComponent<{item}>(Component.{item}) = value;
    }}
";

            string content2 = $@"
    bool {isShow_NAME} => HasComponent(Component.{item});";

            content3 += $@"
                case Component.{item}:
                    {item} _{item} = ({item})entityPrefab.componentDatas[index];
                    _{item}.IdEntity = IdEntity;
                    {item} = _{item};
                    break;";

            module1 += content1;
            module2 += content2;
        }


    //     string module3 = $@"
    // public partial void SetUpDataPrefab(EntityPrefab entityPrefab)
    // {{
    //     for (var i = 0; i < entityPrefab.flags.Count; i++)
    //     {{
    //         SetData(i);
    //     }}
    //     Destroy(entityPrefab);
    //     void SetData(int index)
    //     {{
    //         switch (entityPrefab.flags[index])
    //         {{
    //             {content3}
    //         }}
    //     }}
    // }}
    //     ";


        string scriptContent = $@"
using Sirenix.OdinInspector;
using UnityEngine;
public partial class EntityEditor : MonoBehaviour
{{
    {module1}
    {module2}
    {module3}
}}
";

        string scriptPath = $"Assets/Script/ECS/Engine/EditorEntity/Generate/EntityEditor.cs";
        File.WriteAllText(scriptPath, scriptContent);
        AssetDatabase.Refresh();
    }
    public static void GenerateEntityPrefab()
    {
        ComponentEditorData componentEditorData = new ComponentEditorData();
        componentEditorData.LoadData();
        string[] strs = new string[componentEditorData.componentConfigData.componentConfigItems.Count];
        for (var i = 0; i < strs.Length; i++)
        {
            strs[i] = componentEditorData.componentConfigData.componentConfigItems[i].component.ToString();
        }
        string[] componentNames = strs;

        // string filePath = $"Assets/Script/ECS/Component";
        // string[] componentNames = DirectoryUtility.GetAllFileInFolderAndChild(filePath, "cs");


        string module1 = $@"";
        string module2 = $@"";
        string module3;
        string module4;
        string module5;
        string module6;
        string module7;
        string module8;
        string module9;
        string module10;
        string module11;


        string content1 = "";
        string content2 = "";
        string content3 = "";
        string content6 = "";
        string content7 = "";
        string content8 = "";
        string content9 = "";
        string content10 = "";
        string content11 = "";


        int count = 0;
        foreach (var item in componentNames)
        {
            string isShow_NAME = $"isShow{item}";
            string isShowIf = $"\"isShow{item}\"";

            content1 += $@"
    [ColoredFoldoutGroup(Component.{item}, .5f, .5f, .5f, 1), HideLabel, PropertyOrder({componentEditorData.componentConfigData.componentConfigItems[count++].order})]
    [ShowIf({isShowIf}, true), ShowInInspector]
    private {item} {item};
    ";

            content2 += $@"
    private bool {isShow_NAME} = false;";
            content3 += $@"
        Component.{item},";
            content6 += $@"
            case Component.{item}:
                entityPrefabCache = {item};
                break;";
            content7 += $@"
            case Component.{item}:
                {item} = ({item})entityPrefabCache;
                break;";
            content8 += $@"
            case Component.{item}:
                {isShow_NAME} = false;
                break;";
            content9 += $@"
            case Component.{item}:
                {isShow_NAME} = true;
                break;";
            content10 += $@"
                case Component.{item}:
                    componentDatas.Add({item});
                    break;";
            content11 += $@"
                case Component.{item}:
                    {isShow_NAME} = true;
                    {item} = ({item})componentDatas[i];
                    break;";

        }
        module2 = content2;
        module1 = $@"
        {content1}";
        module2 += $@"
    [HideInInspector] public List<Component> flags;
    [HideInInspector] public List<object> componentDatas;";

        module3 = $@"
    [Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(100)]
    private void AddComponent()
    {{
        isShowListComponent = !isShowListComponent;
    }}

    bool isShowListComponent = false;
    [ValueDropdown(""ListComponent"", DropdownHeight = 200)]
    [ShowIf(""isShowListComponent""), PropertySpace, HideLabel, PropertyOrder(1000)]
    [OnValueChanged(""OnChangeValueSellectAddComponent"")]
    public Component sellectedAddComponent = Component.None;
    public void OnChangeValueSellectAddComponent()
    {{
        if (sellectedAddComponent != Component.None)
        {{
            AddComponent(sellectedAddComponent);
            sellectedAddComponent = Component.None;
        }}
        isShowListComponent = false;
    }}
        ";
        module4 = $@"
    public static Component[] ListComponent = new Component[]
    {{
        Component.None,{content3}
    }};";


        module5 = $@"public static object entityPrefabCache;";

        module6 = $@"
    public partial void CopyComponent(Component component)
    {{
        switch (component)
        {{{content6}
        }}
    }}";

        module7 = $@"
    public partial void PasteComponent(Component component)
    {{
        switch (component)
        {{{content7}
        }}
    }}";

        module8 = $@"
    public partial void RemoveComponent(Component component)
    {{
        flags.Remove(component);
        switch (component)
        {{{content8}
        }}
        EditorUtility.SetDirty(this);
    }}";

        module9 = $@"
    public partial void AddComponent(Component component)
    {{
        if (flags.Contains(component)) return;
        switch (component)
        {{{content9}
        }}
        flags.Add(component);
        EditorUtility.SetDirty(this);
    }}";

        module10 = $@"
    public void SaveData()
    {{
        componentDatas = new List<object>();
        foreach (var item in flags)
        {{
            switch (item)
            {{{content10}
            }}
        }}

        ComponentEditorData componentEditorData = new ComponentEditorData();
        ComponentConfigData componentConfigData = ColoredFoldoutGroupAttributeDrawer.componentConfigData;
        componentEditorData.componentConfigData = componentConfigData;
        componentEditorData.Save();
    }}
    ";
        module11 = $@"
    public void CheckShowComponent()
    {{
        flags ??= new List<Component>();
        for (var i = 0; i < flags.Count; i++)
        {{
            switch (flags[i])
            {{{content11}
            }}
        }}
    }}
    ";


        string scriptContent = $@"
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public partial class EntityPrefab : SerializedMonoBehaviour
{{
    private void OnEnable()
    {{
        CheckShowComponent();
    }}{module1}
    {module2}
    {module3}
    {module4}
    {module5}
    {module6}
    {module7}
    {module8}
    {module9}
    {module10}
    {module11}
}}
";

        string scriptPath = $"Assets/Script/ECS/Engine/EditorEntity/Generate/EntityPrefab.cs";
        File.WriteAllText(scriptPath, scriptContent);
        AssetDatabase.Refresh();
    }
    public static void GenerateArchetype()
    {
        string filePath = $"Assets/Script/ECS/Component";
        string[] componentNames = DirectoryUtility.GetAllFileInFolderAndChild(filePath, "cs");

        string module1 = $@"";
        string module2 = $@"";

        string content1 = $@"";
        string content2 = $@"";

        foreach (var item in componentNames)
        {
            string isShow_NAME = $"isShow{item}";
            string isShowIf = $"\"isShow{item}\"";

            content1 += $@"
            case Component.{item}:
                GetComponent<{item}>(component, entity.idEntity) = ({item})data;
                break;";
            content2 += $@"{item},
    ";
        }


        module1 = $@"
    public void CopyComponentData(EntityDataCache entityDataCache, Entity entity)
    {{
        foreach (var data in entityDataCache.dictDataCopy)
        {{
             if (componentIndices.ContainsKey(data.Key))
             {{
                Handle(data.Key, data.Value);
             }}
        }}
        void Handle(Component component, object data)
        {{
            switch (component)
            {{{content1}
            }}
        }}
       
    }}
        ";

        module2 = $@"
public enum Component
{{
    None,
    {content2}
}}";

    string scriptContent = $@"
public partial class Archetype
{{{module1}
}}
{module2}
";

        string scriptPath = $"Assets/Script/ECS/Engine/EditorEntity/Generate/Archetype.cs";
        File.WriteAllText(scriptPath, scriptContent);
        AssetDatabase.Refresh();
    }
    public static void GenerateArchetypeChunk()
    {
        string filePath = $"Assets/Script/ECS/Component";
        string[] componentNames = DirectoryUtility.GetAllFileInFolderAndChild(filePath, "cs");

        string module1 = $@"";
        string module2 = $@"";

        string content1 = $@"";
        string content2 = $@"";

        foreach (var item in componentNames)
        {
            string isShow_NAME = $"isShow{item}";
            string isShowIf = $"\"isShow{item}\"";

            content1 += $@"
            case Component.{item}:
                ComponentArray<{item}> {item}s = new ComponentArray<{item}>(size);
                componentArrays.Add({item}s);
                break;";

            content2 += $@"
            case Component.{item}:
                ComponentArray<{item}> {item}s = GetComponentArray<{item}>(componentIdx);
                return {item}s.components[entityIndices[idEntity]];";
        }


        module1 = $@"
    private void CreateComponentArray(Component component, ushort size)
    {{
        switch (component)
        {{{content1}
        }}
    }}
        ";

        module2 = $@"
    public object GetComponent(Component component, byte componentIdx, ushort idEntity)
    {{
        switch (component)
        {{{content2}
        }}
        return null;
    }}
        ";

        string scriptContent = $@"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ArchetypeChunk
{{{module1}
{module2}
}}
";

        string scriptPath = $"Assets/Script/ECS/Engine/EditorEntity/Generate/ArchetypeChunk.cs";
        File.WriteAllText(scriptPath, scriptContent);
        AssetDatabase.Refresh();
    }

    public static void GenerateComponentData(string directory, List<string> componentNames)
    {
        string filePath = $"Assets/Script/ECS/Component" + directory;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(filePath);
            AssetDatabase.Refresh();
        }
        foreach (var item in componentNames)
        {
            string scriptContent = $@"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct {item}Component : IComponentData
{{
    private Entity entity;
    public Entity GetEntity()
    {{
        return entity;
    }}

    public void SetEntity(Entity value)
    {{
        this.entity = value;
    }}

    public void Start()
    {{

    }}
}}";
            string scriptPath = $"{filePath}/{item}Component.cs";
            File.WriteAllText(scriptPath, scriptContent);
        }
        AssetDatabase.Refresh();

    }

    public static void GenerateSystem(string directory, List<string> componentNames)
    {
        string filePath = $"Assets/Script/ECS/System" + directory;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(filePath);
            AssetDatabase.Refresh();
        }
        foreach (var item in componentNames)
        {
            string scriptContent = $@"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class {item}System : SystemBase
{{
    public override void Start()
    {{
        entityQuery = new EntityQuery(Component.None);
    }}
    
    public override void Execute()
    {{

    }}
}}";
            string scriptPath = $"{filePath}/{item}System.cs";
            File.WriteAllText(scriptPath, scriptContent);
        }
        AssetDatabase.Refresh();

    }

    public static void GenerateSystemManager()
    {
        string filePath = $"Assets/Script/ECS/System";
        string[] names = DirectoryUtility.GetAllFileInFolderAndChild(filePath);

        string content1 = @$"";
        string content2 = @$"";

        foreach (var item in names)
        {
            content1 += @$"
                case ESystem.{item}:
                    systemBase = new {item}();
                    break;";
            content2 += @$"
    {item},";
        }

        string scriptContent = $@"
public partial class SystemManager
{{
    public partial void CreateSystem(params ESystem[] systems)
    {{
        foreach (var item in systems)
        {{
            SystemBase systemBase = null;
            switch (item)
            {{{content1}
            }}
            systemBase.Start();
            listSystem.Add(systemBase);
            systemIndices.Add(item, count++);
        }}
    }}
}}

public enum ESystem
{{{content2}
}}
";

        string scriptPath = $"Assets/Script/ECS/Engine/EditorEntity/Generate/SystemManager.cs";
        File.WriteAllText(scriptPath, scriptContent);
        AssetDatabase.Refresh();
    }

    public static void RemoveFile(params string[] scriptNames)
    {
        string rootFolderPath = "Assets/Script/ECS"; // Thư mục gốc của các thư mục chứa tệp

        foreach (string scriptName in scriptNames)
        {
            string fullPath = FindScriptAndGetPath(rootFolderPath, scriptName);
            if (!string.IsNullOrEmpty(fullPath))
            {
                DeleteFile(fullPath);
                CheckAndDeleteEmptyFolder(Path.GetDirectoryName(fullPath));
            }
        }

        AssetDatabase.Refresh();

        string FindScriptAndGetPath(string folderPath, string scriptName)
        {
            string[] files = Directory.GetFiles(folderPath, scriptName + ".cs", SearchOption.AllDirectories);
            if (files.Length > 0)
            {
                return files[0]; // Trả về đường dẫn của tệp đầu tiên tìm thấy
            }
            return string.Empty;
        }

        void DeleteFile(string filePath)
        {
            File.Delete(filePath);
            UnityEngine.Debug.Log("Deleted file: " + filePath);
        }

        static void CheckAndDeleteEmptyFolder(string folderPath)
        {
            if (Directory.GetFiles(folderPath).Length == 0 && Directory.GetDirectories(folderPath).Length == 0)
            {
                Directory.Delete(folderPath);
                UnityEngine.Debug.Log("Deleted empty folder: " + folderPath);
                CheckAndDeleteEmptyFolder(Path.GetDirectoryName(folderPath)); // Kiểm tra và xoá cả thư mục cha nếu nó trống
            }
        }
    }


}


