using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntityManager
{
    public List<EntityEditor> entityEditors;
    public ushort[] editorIndice;
    public ushort countEntity = 0;
    private ArchetypeManager archetypeManager;

    public EntityManager(ArchetypeManager archetypeManager)
    {
        this.archetypeManager = archetypeManager;
        entityEditors = new List<EntityEditor>();
        editorIndice = new ushort[5000];
    }

    public Entity CreateEntity(params Component[] components)
    {
        Flag flag = new Flag();
        flag.AddComponents(components);
        
        Archetype archetype = archetypeManager.GetArchetype(flag);
        GameObject go = new GameObject("Entity " + countEntity);
        Entity entity = new Entity(flag, ++countEntity, go);
        archetype.AddEntity(entity);
        return entity;
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