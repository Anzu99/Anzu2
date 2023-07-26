// using System;
// using System.Collections.Generic;

// public partial class ComponentManager
// {
//     public Dictionary<Component, object> dicsComponent;

//     public ComponentManager()
//     {
//         dicsComponent = new Dictionary<Component, object>();
//     }

//     private void CreateComponentArray<T>(ushort maxCapacity, Component component) where T : struct, IComponentData
//     {
//         ComponentArray<T> componentArray = new ComponentArray<T>(maxCapacity);
//         dicsComponent.Add(component, componentArray);
//     }

//     public void CreateComponents(Component[] components, ushort idEntity)
//     {
//         foreach (var item in components)
//         {
//             if (dicsComponent.ContainsKey(item))
//             {
//                 CreateComponent(item, idEntity);
//             }
//             else
//             {
//                 CreateComponentArray(item);
//                 CreateComponent(item, idEntity);
//             }
//         }
//     }

//     public void CreateComponents(Component component, ushort idEntity)
//     {
//         if (dicsComponent.ContainsKey(component))
//         {
//             CreateComponent(component, idEntity);
//         }
//         else
//         {
//             CreateComponentArray(component);
//             CreateComponent(component, idEntity);
//         }
//     }

//     private void RemoveComponent<T>(Component component, ushort idEntity) where T : struct, IComponentData
//     {
//         if (dicsComponent.ContainsKey(component))
//         {
//             ComponentArray<T> componentArray = dicsComponent[component] as ComponentArray<T>;
//             componentArray.RemoveComponent(idEntity);
//         }
//     }

//     public ref T GetComponent<T>(Component flag, ushort idEntity) where T : struct, IComponentData
//     {
//         ComponentArray<T> componentArray = dicsComponent[flag] as ComponentArray<T>;
//         return ref componentArray.GetComponentFromArray(idEntity);
//     }

//     public bool HasComponentArray(Component flag)
//     {
//         if (dicsComponent.ContainsKey(flag)) return true;
//         else
//         {
//             return dicsComponent.ContainsKey(flag);
//         }
//     }
//     public ref T[] GetComponents<T>(Component flag) where T : struct, IComponentData
//     {
//         ComponentArray<T> componentArray = dicsComponent[flag] as ComponentArray<T>;
//         return ref componentArray.GetComponentArray();
//     }
//     public int GetComponentsLenght<T>(Component flag) where T : struct, IComponentData
//     {
//         ComponentArray<T> componentArray = dicsComponent[flag] as ComponentArray<T>;
//         return componentArray.count;
//     }
// }

