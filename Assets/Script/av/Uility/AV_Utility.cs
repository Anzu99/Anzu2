using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Linq;

namespace AV
{
    #region get color
    public class AV_CONVERT_HEXCOLOR : MonoBehaviour
    {
        public static Color GetColor(string stringValue, float alpha = -1)
        {
            Color color = Color.white;
            if (ColorUtility.TryParseHtmlString("#" + stringValue, out color))
            {
                if (alpha == -1)
                    return color;
                else
                {
                    color.a = alpha;
                    return color;
                }
            }
            return color;
        }
    }

    #endregion

    #region invoke
    public static class AV_INVOKE
    {
        public static void Invoke(this MonoBehaviour mb, Action action, float delay)
        {
            mb.StartCoroutine(InvokeRoutine(action, delay));
        }

        private static IEnumerator InvokeRoutine(Action a, float delay)
        {
            yield return new WaitForSeconds(delay);
            a();
        }
    }
    #endregion

    #region do somthing with coroutine
    public static class AV_COROUTINE
    {
        public static void DoInEachFrame(this MonoBehaviour mb, Action a, int amountFrame)
        {
            mb.StartCoroutine(InvokeRoutine(a, amountFrame));
        }

        private static IEnumerator InvokeRoutine(Action a, int amountFrame)
        {
            int i = 0;
            while (i < amountFrame)
            {
                a();
                i++;
                yield return null;
            }
        }

        ///<summary>
        /// do something after a few frames
        ///</summary>
        public static void DoInSpecifiedFrame(this MonoBehaviour mb, Action a, int amountFrame, int frameStep)
        {
            mb.StartCoroutine(InvokeRoutine(a, amountFrame, frameStep));
        }
        private static IEnumerator InvokeRoutine(Action a, int amountFrame, int frameStep)
        {
            int totalFrame = frameStep * amountFrame;
            int i = 0;
            int countAction = 0;
            while (i < totalFrame)
            {
                if (i == countAction * frameStep)
                {
                    a();
                    countAction++;
                }
                i++;
                yield return null;
            }
        }

    }
    #endregion

    #region get position, angle 
    public static class AV_GET_POSITION
    {
        ///<summary>
        ///  
        ///</summary>
        public static Vector2 GetPosWithAngleAndDistance(Vector2 originPosition, float angle, float distance)
        {
            var x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            var y = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            return new Vector2(originPosition.x + x, originPosition.y + y);
        }

        public static Vector3 GetPosWithAngleAndDistance(Vector3 originPosition, float angle, float distance)
        {
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            return originPosition + q * Vector3.up * distance;
        }


    }

    public static class AV_GET_ANGLE
    {
        ///<summary>
        /// finding an angle between two points (target, player) 
        ///</summary>
        public static float GetAngle2Point(Vector2 point1, Vector2 point2)
        {
            return Mathf.Atan2(point2.y - point1.y, point2.x - point1.x) * 180 / Mathf.PI;
        }
        public static float DirectionToAngle(Vector2 Direction)
        {
            return Mathf.Atan2(Direction.x, Direction.y) / Mathf.PI * 180;
        }

    }

    #endregion

    #region Draw debug
    public class AV_DRAW_DEBUG
    {
        public static void DrawRectangleInGizmos(Vector3 position, Vector2 extent, Quaternion orientation, Color color)
        {
            Gizmos.color = color;
            Vector3 rightOffset = Vector3.right * extent.y * 0.5f;
            Vector3 upOffset = Vector3.up * extent.x * 0.5f;

            Vector3 offsetA = orientation * (rightOffset + upOffset);
            Vector3 offsetB = orientation * (-rightOffset + upOffset);
            Vector3 offsetC = orientation * (-rightOffset - upOffset);
            Vector3 offsetD = orientation * (rightOffset - upOffset);

            Gizmos.DrawLine(position + offsetA, position + offsetB);
            Gizmos.DrawLine(position + offsetB, position + offsetC);
            Gizmos.DrawLine(position + offsetC, position + offsetD);
            Gizmos.DrawLine(position + offsetD, position + offsetA);
        }

        public static void DrawRectangle(Vector3 position, Vector2 extent, Color color)
        {
            Vector3 rightOffset = Vector3.right * extent.x * 0.5f;
            Vector3 upOffset = Vector3.up * extent.y * 0.5f;

            Vector3 offsetA = rightOffset + upOffset;
            Vector3 offsetB = -rightOffset + upOffset;
            Vector3 offsetC = -rightOffset - upOffset;
            Vector3 offsetD = rightOffset - upOffset;

            Debug.DrawLine(position + offsetA, position + offsetB, color);
            Debug.DrawLine(position + offsetB, position + offsetC, color);
            Debug.DrawLine(position + offsetC, position + offsetD, color);
            Debug.DrawLine(position + offsetD, position + offsetA, color);
        }
        public static void DrawRectangle(Vector3 position, Vector2 extent, Quaternion angle, Color color)
        {

            Vector3 rightOffset = Vector3.right * extent.x * 0.5f;
            Vector3 upOffset = Vector3.up * extent.y * 0.5f;

            Vector3 offsetA = angle * (rightOffset + upOffset);
            Vector3 offsetB = angle * (-rightOffset + upOffset);
            Vector3 offsetC = angle * (-rightOffset - upOffset);
            Vector3 offsetD = angle * (rightOffset - upOffset);

            Debug.DrawLine(position + offsetA, position + offsetB, color);
            Debug.DrawLine(position + offsetB, position + offsetC, color);
            Debug.DrawLine(position + offsetC, position + offsetD, color);
            Debug.DrawLine(position + offsetD, position + offsetA, color);
        }


        public static void DrawRect(Vector3 min, Vector3 max, Vector3 angle, Color color)
        {
            Debug.DrawLine(min, new Vector3(min.x, max.y), color);
            Debug.DrawLine(new Vector3(min.x, max.y), max, color);
            Debug.DrawLine(max, new Vector3(max.x, min.y), color);
            Debug.DrawLine(min, new Vector3(max.x, min.y), color);
        }
        public static void DrawRect(Vector3 min, Vector3 max, Color color)
        {
            Debug.DrawLine(min, new Vector3(min.x, max.y), color);
            Debug.DrawLine(new Vector3(min.x, max.y), max, color);
            Debug.DrawLine(max, new Vector3(max.x, min.y), color);
            Debug.DrawLine(min, new Vector3(max.x, min.y), color);
        }

        public static void DrawRect(Vector3 min, Vector3 max, Color color, float duration)
        {
            Debug.DrawLine(min, new Vector3(min.x, max.y), color, duration);
            Debug.DrawLine(new Vector3(min.x, max.y), max, color, duration);
            Debug.DrawLine(max, new Vector3(max.x, min.y), color, duration);
            Debug.DrawLine(min, new Vector3(max.x, min.y), color, duration);
        }

        public static void DrawRect(Rect rect, Color color)
        {
            DrawRect(rect.min, rect.max, color);
        }

        public static void DrawRect(Rect rect, Color color, float duration)
        {
            DrawRect(rect.min, rect.max, color, duration);
        }
    }
    #endregion

    #region Get Path
#if UNITY_EDITOR
    public static class AV_GET_PATH
    {
        [Obsolete]
        public static string GetPathWithGameObject(GameObject _gameobject)
        {
            UnityEngine.Object parentObject = EditorUtility.GetPrefabParent(_gameobject);
            string path = "";
            path = AssetDatabase.GetAssetPath(parentObject);
            return path;
        }
    }
#endif
    #endregion

    #region Prefab Utility
    //overides
    //PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
    #endregion

    #region Enum Extension
    public static class AV_ENUM_EXTENSION
    {
        public static T Next<T>(this T v) where T : struct
        {
            return Enum.GetValues(v.GetType()).Cast<T>().Concat(new[] { default(T) }).SkipWhile(e => !v.Equals(e)).Skip(1).First();
        }

        public static T Previous<T>(this T v) where T : struct
        {
            return Enum.GetValues(v.GetType()).Cast<T>().Concat(new[] { default(T) }).Reverse().SkipWhile(e => !v.Equals(e)).Skip(1).First();
        }

        public static byte CountOnBits(ulong x)
        {
            byte count = 0;
            while (x != 0)
            {
                if ((x & 1) != 0) count++;
                x = x >> 1;
            }
            return count;
        }
        public static byte CountOnBits(int x)
        {
            byte count = 0;
            while (x != 0)
            {
                if ((x & 1) != 0) count++;
                x = x >> 1;
            }
            return count;
        }
    }

    #endregion



    #region Wacth
#if UNITY_EDITOR
    public static class AV_WATCH
    {
        static Dictionary<string, System.Diagnostics.Stopwatch> dicsWatch = new Dictionary<string, System.Diagnostics.Stopwatch>();
        public static void Start(string key)
        {
            if (dicsWatch.ContainsKey(key)) dicsWatch.Remove(key);
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            dicsWatch.Add(key, stopwatch);

        }

        public static void Stop(string key)
        {
            dicsWatch[key].Stop();
            TimeSpan elapsedTime = dicsWatch[key].Elapsed;
            Debug.LogError(key + "  " + elapsedTime.Ticks);
        }
    }
#endif
    #endregion

    #region Server data
    public class ConvertDataFromServer<T>
    {
        public static T GetData(string strData)
        {
            RoughData dataFromServer = Newtonsoft.Json.JsonConvert.DeserializeObject<RoughData>(strData);
            if (dataFromServer.status)
                return dataFromServer.data;
            else
                return default;
        }

        public class RoughData
        {
            public bool status;
            public T data;
        }
    }
    #endregion

}

