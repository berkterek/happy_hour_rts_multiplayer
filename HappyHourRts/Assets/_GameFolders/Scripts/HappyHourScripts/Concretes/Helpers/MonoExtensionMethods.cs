using UnityEngine;

namespace HappyHourRts.Helpers
{
    public static class MonoExtensionMethods
    {
        public static void GetReference<T>(this MonoBehaviour monoBehaviour, ref T value) where T : Object
        {
            if (value != null) return;
            
            value = monoBehaviour.transform.GetComponentInChildren<T>();
        }
    }
}