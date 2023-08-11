using System;
using System.Collections;
using System.Collections.Generic;
using ANZU;
using Sirenix.OdinInspector;
using UnityEngine;

using System.Linq;

public partial class World : MonoBehaviour
{
    public static World instance;

    [ShowInInspector]
    public static EntityManager entityManager;
    [ShowInInspector]
    public static ArchetypeManager archetypeManager;
    private SystemManager systemManager;

    private void Awake()
    {
        instance = this;
        archetypeManager = new ArchetypeManager();
        entityManager = new EntityManager(archetypeManager);
        systemManager = new SystemManager();
        OnStart();
    }
    MoveSystem moveSystem;
    [SerializeField] private bool az;
    private void OnStart()
    {
        CreateSystem(ESystem.MoveSystem, ESystem.InputSystem);
        CreateEntity(Component.TransformComponent, Component.InputComponent);

        // CreateSystem(ESystem.MoveSystem);
        // entityManager.LoadEntity(PathConfig.Entity.player1, null);
        // entityManager.CreateEntity(Component.InfoComponent, Component.MovementComponent);

        // SpawnHandle();
    }

    public void SpawnHandle()
    {
        if (az)
        {
            StartCoroutine(Spawn());
            IEnumerator Spawn()
            {
                yield return new WaitForSeconds(.5f);
                int index = 0;
                while (index < 1000)
                {
                    yield return null;
                    for (var i = 0; i < 200; i++)
                    {
                        entityManager.CreateEntity(Component.TransformComponent, Component.MoveComponent);
                        index++;
                    }
                }
            }
        }
        else
        {
            StartCoroutine(Spawn());
            IEnumerator Spawn()
            {
                yield return new WaitForSeconds(.5f);
                int index = 0;
                while (index < 4000)
                {
                    yield return null;
                    for (var i = 0; i < 200; i++)
                    {
                        GameObject go = new GameObject(index.ToString());
                        go.AddComponent<ObjMoveTest>();
                        index++;
                    }
                }
            }
        }
    }

    private void Update()
    {
        UpdateSystem();
        UpdateEngine();
    }

    #region Entity
    public void CreateEntity(params Component[] components)
    {
        entityManager.CreateEntity(components);
    }
    #endregion

    #region System
    public void CreateSystem(params ESystem[] systems)
    {
        systemManager.CreateSystem(systems);
    }
    private void UpdateSystem()
    {
        systemManager.UpdateSystem();
    }
    public void RemoveSystem(params ESystem[] systems)
    {
        systemManager.RemoveSystem(systems);
    }
    public void ActiveSystem(bool active, params ESystem[] systems)
    {
        systemManager.ActiveSystem(active, systems);
    }
    #endregion

    #region Engine Update
    private static Queue<Action> updateEngineAction = new Queue<Action>();
    public void UpdateEngine()
    {
        lock (updateEngineAction)
        {
            while (updateEngineAction.Count > 0)
            {
                Action action = updateEngineAction.Dequeue();
                action.Invoke();
            }
        }
    }

    public static void AddEngineUpdateAction(Action action)
    {
        lock (updateEngineAction)
        {
            updateEngineAction.Enqueue(action);
        }
    }
    #endregion
}
