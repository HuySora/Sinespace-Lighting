namespace ProjectName.Extension
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class GameObjectExtension
    {
        public static bool IsHidden(this GameObject go)
            => (go.hideFlags & HideFlags.HideInHierarchy) != 0;
    }
}