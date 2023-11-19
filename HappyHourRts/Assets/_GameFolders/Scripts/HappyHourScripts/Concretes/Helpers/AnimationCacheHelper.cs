using UnityEngine;

namespace HappyHourRts.Helpers
{
    public static class AnimationCacheHelper
    {
        public static int DirectionY { get; }
        public static int DirectionX { get; }
        public static int IsRunning { get; }
        public static int IsAttacking { get; }
        public static int Dying { get; }
        
        static AnimationCacheHelper()
        {
            DirectionY = Animator.StringToHash("DirectionY");
            DirectionX = Animator.StringToHash("DirectionX");
            IsRunning = Animator.StringToHash("IsRunning");
            IsAttacking = Animator.StringToHash("IsAttacking");
            Dying = Animator.StringToHash("Dying");
        }
    }
}