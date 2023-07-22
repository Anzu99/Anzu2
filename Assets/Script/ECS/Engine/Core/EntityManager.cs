using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntityManager
{
    public Entity[] entities;
    public List<EntityEditor> entityEditors;
    public int[] editorIndice;
    public ushort countEntity = 0;

    public EntityManager()
    {
        entityEditors = new List<EntityEditor>();
        entities = new Entity[ConfigCapacity.MaxEnities];
        editorIndice = new int[ConfigCapacity.MaxEnities];
    }
    public Entity CreateEntity(params Component[] components)
    {
        GameObject go = new GameObject("Entity " + countEntity);
        entities[++countEntity] = new Entity(components, countEntity, go);
        entities[countEntity].ComponentInitalize(components);
        CreateEntityEditor(go);
        AddToArchetype();
        return entities[countEntity];
    }

    public Entity CreateEntity(Transform parent, params Component[] components)
    {
        GameObject go = new GameObject("Entity " + countEntity);
        go.transform.SetParent(parent);
        entities[++countEntity] = new Entity(components, countEntity, go);
        entities[countEntity].ComponentInitalize(components);
        CreateEntityEditor(go);
        AddToArchetype();
        return entities[countEntity];
    }

    public Entity CreateEntity(string name, params Component[] components)
    {
        GameObject go = new GameObject(name);
        entities[++countEntity] = new Entity(components, countEntity, go);
        entities[countEntity].ComponentInitalize(components);
        CreateEntityEditor(go);
        AddToArchetype();
        return entities[countEntity];
    }

    public Entity LoadEntity(string path, Transform parent = null)
    {
        EntityPrefab entityPrefab = Resources.Load<EntityPrefab>(path);
        entityPrefab = UnityEngine.Object.Instantiate(entityPrefab, parent);
        entities[++countEntity] = new Entity(countEntity, entityPrefab);
        CreateEntityEditor(entityPrefab);
        entities[countEntity].ComponentInitalize(entityPrefab.flags.ToArray());
        AddToArchetype();
        return entities[countEntity];
    }
    public Entity LoadEntity(string path, string name = "", Transform parent = null)
    {
        EntityPrefab entityPrefab = Resources.Load<EntityPrefab>(path);
        entityPrefab = UnityEngine.Object.Instantiate(entityPrefab, parent);
        if (!string.IsNullOrEmpty(name)) entityPrefab.name = name;
        entities[++countEntity] = new Entity(countEntity, entityPrefab);
        // CreateEntityEditor(entityPrefab);
        entities[countEntity].ComponentInitalize(entityPrefab.flags.ToArray());
        AddToArchetype();
        return entities[countEntity];
    }
    public Entity LoadEntity(string path, Vector3 position, Vector3 rotation, Transform parent = null)
    {
        EntityPrefab entityPrefab = Resources.Load<EntityPrefab>(path);
        entityPrefab = UnityEngine.Object.Instantiate(entityPrefab, position, Quaternion.Euler(rotation), parent);
        entities[++countEntity] = new Entity(countEntity, entityPrefab);
        CreateEntityEditor(entityPrefab);
        entities[countEntity].ComponentInitalize(entityPrefab.flags.ToArray());
        AddToArchetype();
        return entities[countEntity];
    }

    public void AddToArchetype()
    {
        World.archetypeManager.AddToArchetype(entities[countEntity]);
    }
    public void CreateEntityEditor(GameObject go)
    {
        EntityEditor _entityEditor = go.AddComponent<EntityEditor>();
        _entityEditor.IdEntity = countEntity;
        entityEditors.Add(_entityEditor);
        editorIndice[countEntity] = entityEditors.Count - 1;
        go.AddComponent<EditorForceRepaint>();
    }

    public void CreateEntityEditor(EntityPrefab entityPrefab)
    {
        EntityEditor _entityEditor = entityPrefab.gameObject.AddComponent<EntityEditor>();
        _entityEditor.IdEntity = countEntity;
        entityEditors.Add(_entityEditor);
        editorIndice[countEntity] = entityEditors.Count - 1;
        entityPrefab.gameObject.AddComponent<EditorForceRepaint>();
        _entityEditor.SetUpDataPrefab(entityPrefab);
    }



}