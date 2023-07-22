using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(FbFComponent))]
public class FbFComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FbFComponent fbFComponent = (FbFComponent)target;
        base.OnInspectorGUI();
        GUILayout.Space(10);
        if (GUILayout.Button("Play"))
        {
            fbFComponent.EditorPlay();
        }
        GUILayout.Space(1);
        if (GUILayout.Button("Stop"))
        {
            fbFComponent.EditorPause();
        }
        GUILayout.Space(1);
        if (GUILayout.Button("Restart"))
        {
            fbFComponent.EditorRestart();
        }
        EditorUtility.SetDirty(target);
    }
}

[ExecuteInEditMode]
#endif
public class FbFComponent : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] arraySprite;
    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private bool isLoop = false;
    [SerializeField] private float timeCycle = 1;
    [SerializeField] private float delay = 0;

    [SerializeField] private List<FbFComponent> fbfGroup = new List<FbFComponent>();

    private Action callWhenEndCycle;
    protected void SetListSprite(Sprite[] sprites)
    {
        arraySprite = sprites;
        timePerFrame = timeCycle / arraySprite.Length;
    }
    protected bool SetPlayOnEnable { set => playOnAwake = value; }
    protected bool SetLoop { set => isLoop = value; }
    protected void SetTime(float time)
    {
        timeCycle = time;
        timePerFrame = timeCycle / arraySprite.Length;
    }
    protected float SetDelay { set => delay = value; }

    private bool isPlay = false;
    private float timeSinceStartup;

    private int currentFrame;
    private float timePerFrame = 0;
    private float currentTimePerFrame;

    private void Awake()
    {
        isPlay = playOnAwake;
        SetUp();
    }
    public void SetUp()
    {
        currentFrame = 0;
        timeSinceStartup = 0;
        currentTimePerFrame = 0;
        if (arraySprite != null)
            timePerFrame = timeCycle / arraySprite.Length;
    }

    private void Update()
    {
        if (isPlay)
        {
            Process();
        }
    }

    public void Play()
    {
        currentTimePerFrame = 0;
        timeSinceStartup = 0;
        isPlay = true;
    }
    public void Play(Action _callWhenEndCycle)
    {
        callWhenEndCycle = _callWhenEndCycle;
        Play();
    }
    public void ClearCallBack()
    {
        callWhenEndCycle = null;
    }

    public void Stop()
    {
        isPlay = false;
    }

    public void Process()
    {
        timeSinceStartup += Time.deltaTime;
        if (timeSinceStartup > delay)
        {
            currentTimePerFrame -= Time.deltaTime;
            if (currentTimePerFrame <= 0)
            {
                spriteRenderer.sprite = arraySprite[currentFrame];
                currentTimePerFrame = timePerFrame;
                currentFrame++;
                if (currentFrame == arraySprite.Length)
                {
                    callWhenEndCycle?.Invoke();
                    currentFrame = 0;
                    if (!isLoop)
                        isPlay = false;
                }
            }
        }
    }

#if UNITY_EDITOR

    #region  Group Editor
    public void GroupHandle()
    {
        FbFComponent[] fbFComponents = GetComponentsInChildren<FbFComponent>();
        foreach (var item in fbFComponents)
        {
            if (!fbfGroup.Contains(item) && item != this)
            {
                fbfGroup.Add(item);
            }
        }
    }

    public void GroupPlay()
    {
        if (fbfGroup != null)
        {
            foreach (var item in fbfGroup)
            {
                item.Play();
            }
        }
    }
    public void GroupPause()
    {
        if (fbfGroup != null)
        {
            foreach (var item in fbfGroup)
            {
                item.isPlay = false;
            }
        }
    }

    public void GroupRestart()
    {
        if (fbfGroup != null)
        {
            foreach (var item in fbfGroup)
            {
                item.EditorRestart();
            }
        }
    }
    #endregion

    #region  Editor
    public void EditorPlay()
    {
        GroupHandle();
        CheckIfGroupComponent();
        if (spriteRenderer == null)
        {
            GroupPlay();
            return;
        }
        isPlay = true;
    }
    public void EditorRestart()
    {
        GroupHandle();
        CheckIfGroupComponent();
        if (spriteRenderer == null)
        {
            GroupRestart();
            return;
        }
        SetUp();
        EditorPlay();
    }
    public void EditorPause()
    {
        isPlay = false;
        GroupPause();
    }
    #endregion

    public void CheckIfGroupComponent()
    {
        if (fbfGroup.Count > 0)
        {
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                DestroyImmediate(spriteRenderer);
            }
        }
    }
    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }


#endif

}
