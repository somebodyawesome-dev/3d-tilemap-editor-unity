using System;
using UnityEngine;

namespace Editor.mono
{
    public static class VectorUtility
    {
        public static Vector3 Truncate(this Vector3 vec, Vector3 offset = new Vector3())
        {

            return new Vector3(

                (int)Math.Truncate(vec.x) + offset.x,
                (int)Math.Truncate(vec.y) + offset.y,
                (int)Math.Truncate(vec.z) + offset.z
            );
        }
    }
}
