
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ArchetypeChunk
{
    private void CreateComponentArray(Component component, ushort size)
    {
        switch (component)
        {
            case Component.InputComponent:
                ComponentArray<InputComponent> InputComponents = new ComponentArray<InputComponent>(size);
                componentArrays.Add(InputComponents);
                break;
            case Component.MoveComponent:
                ComponentArray<MoveComponent> MoveComponents = new ComponentArray<MoveComponent>(size);
                componentArrays.Add(MoveComponents);
                break;
            case Component.TransformComponent:
                ComponentArray<TransformComponent> TransformComponents = new ComponentArray<TransformComponent>(size);
                componentArrays.Add(TransformComponents);
                break;
        }
    }
        

    public object GetComponent(Component component, byte componentIdx, ushort idEntity)
    {
        switch (component)
        {
            case Component.InputComponent:
                ComponentArray<InputComponent> InputComponents = GetComponentArray<InputComponent>(componentIdx);
                return InputComponents.components[entityIndices[idEntity]];
            case Component.MoveComponent:
                ComponentArray<MoveComponent> MoveComponents = GetComponentArray<MoveComponent>(componentIdx);
                return MoveComponents.components[entityIndices[idEntity]];
            case Component.TransformComponent:
                ComponentArray<TransformComponent> TransformComponents = GetComponentArray<TransformComponent>(componentIdx);
                return TransformComponents.components[entityIndices[idEntity]];
        }
        return null;
    }
        
}
