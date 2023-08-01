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
    public static ComponentManager componentManager;
    [ShowInInspector]
    public static ArchetypeManager archetypeManager;
    private SystemManager systemManager;

    private void Awake()
    {
        ShowFPS.ShowFPSHandle();
        instance = this;
        archetypeManager = new ArchetypeManager();
        entityManager = new EntityManager(archetypeManager);
        systemManager = new SystemManager();
        OnStart();
    }

    private void OnStart()
    {
        // CreateSystem(ESystem.MoveSystem);
        // entityManager.LoadEntity(PathConfig.Entity.player1, null);
        // entityManager.CreateEntity(Component.InfoComponent, Component.MovementComponent);
        // StartCoroutine(Spawn());
        // IEnumerator Spawn()
        // {
        //     yield return new WaitForSeconds(.5f);
        //     int index = 0;
        //     while (index < 8000)
        //     {
        //         yield return null;
        //         for (var i = 0; i < 20; i++)
        //         {
        //             entityManager.LoadEntity(PathConfig.Entity.player1, index.ToString());
        //             index++;
        //         }
        //     }
        // }

    }

    private void Update()
    {
        UpdateSystem();
    }

    public static Entity GetEntity(ushort idEntity)
    {
        return entityManager.entities[idEntity];
    }

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
}
