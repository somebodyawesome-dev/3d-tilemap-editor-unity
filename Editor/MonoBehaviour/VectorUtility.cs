using System;
using UnityEngine;

namespace Editor.MonoBehaviour
{
    public static class VectorUtility
    {
        public static Vector3 Truncate(this Vector3 vec, Vector3 offset = new Vector3())
        {
            return new Vector3(
                (float) Math.Truncate(vec.x) + offset.x,
                (float) Math.Truncate(vec.y) + offset.y,
                (float) Math.Truncate(vec.z) + offset.z
            );
        }
    }
}