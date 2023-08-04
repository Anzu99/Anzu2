using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntityManager
{
    public List<EntityEditor> entityEditors;
    public int[] editorIndice;
    public ushort countEntity = 0;
    private ArchetypeManager archetypeManager;

    public EntityManager(ArchetypeManager archetypeManager)
    {
        this.archetypeManager = archetypeManager;
        entityEditors = new List<EntityEditor>();
        editorIndice = new int[5000];
    }

    public Entity CreateEntity(params Component[] components)
    {
        Flag flag = new Flag();
        flag.AddComponents(components);

        Archetype archetype = archetypeManager.GetArchetype(flag);
        GameObject go = new GameObject("Entity " + countEntity);
        Entity entity = new Entity(archetype, ++countEntity, go);
        return entity;
    }

    public void CreateEntityEditor(GameObject go, Entity entity)
    {
        EntityEditor _entityEditor = go.AddComponent<EntityEditor>();
        _entityEditor.Entity = entity;
        entityEditors.Add(_entityEditor);
        editorIndice[countEntity] = entityEditors.Count - 1;
        go.AddComponent<EditorForceRepaint>();
    }

    public Entity GetEntity(ushort idEntity)
    {
        return archetypeManager.GetEntity(idEntity);
    }

}