using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntityManager
{
#if UNITY_EDITOR
    public List<EntityEditor> entityEditors;
    public int[] editorIndice;
#endif
    public ushort countEntity = 0;
    private ArchetypeManager archetypeManager;

    public EntityManager(ArchetypeManager archetypeManager)
    {
#if UNITY_EDITOR
        entityEditors = new List<EntityEditor>();
#endif
        this.archetypeManager = archetypeManager;
        editorIndice = new int[5000];
    }

    public Entity CreateEntity(params Component[] components)
    {
        Flag flag = new Flag();
        flag.AddComponents(components);

        Archetype archetype = archetypeManager.GetArchetype(flag);
        GameObject go = new GameObject("Entity " + countEntity);
        Entity entity = new Entity(archetype, ++countEntity, go);

#if UNITY_EDITOR
        CreateEntityEditor(go, entity);
#endif

        return entity;
    }

#if UNITY_EDITOR
    public void CreateEntityEditor(GameObject go, Entity entity)
    {
        EntityEditor _entityEditor = go.AddComponent<EntityEditor>();
        _entityEditor.Entity = entity;
        entityEditors.Add(_entityEditor);
        editorIndice[countEntity] = entityEditors.Count - 1;
        go.AddComponent<EditorForceRepaint>();
    }
#endif

}