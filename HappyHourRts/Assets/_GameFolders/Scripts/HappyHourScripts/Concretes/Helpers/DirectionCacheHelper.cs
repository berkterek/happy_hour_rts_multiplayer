using UnityEngine;

namespace HappyHourRts.Helpers
{
    public static class DirectionCacheHelper
    {
        public static Vector2 Vector2Zero { get; }
        public static Vector3 Vector3Zero { get; }
        public static Vector3Int Vector3IntZero { get; }
        public static Vector2 Left { get; }
        public static Vector2 Right { get; }
        public static Vector2 Up { get; }
        public static Vector2 Down { get; }
        public static Quaternion Identity { get; }
        
        static DirectionCacheHelper()
        {
            Vector2Zero = Vector2.zero;
            Vector2Zero = Vector3.zero;
            Vector3IntZero = Vector3Int.zero;
            Left = Vector2.left;
            Right = Vector2.right;
            Up = Vector2.up;
            Down = Vector2.down;
            Identity = Quaternion.identity;
        }
    }
}