namespace ProjectName.EditorTools
{
    using UnityEngine;

    public class EditorSetActive : EditorAwake
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        public bool Enabled = false;

        public override void Awake()
        {
            gameObject.SetActive(Enabled);
        }
#endif
    }
}