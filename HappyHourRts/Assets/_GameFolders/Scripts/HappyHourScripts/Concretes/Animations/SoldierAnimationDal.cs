using HappyHourRts.Abstracts.Animations;
using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Helpers;
using UnityEngine;

namespace HappyHourRts.Animations
{
    public class SoldierAnimationDal : ISoldierAnimationDal
    {
        Animator _animator;

        public bool IsResourceCollectingNow  => _animator.GetBool(AnimationCacheHelper.IsAttacking);

        public void SetClickableController(IClickableController clickableController)
        {
            _animator = clickableController.transform.GetComponentInChildren<Animator>();
        }

        public void DirectionSetterAnimation(Vector3 value)
        {
            _animator.SetFloat(AnimationCacheHelper.DirectionY, value.y, 0.3f, Time.deltaTime);
            _animator.SetFloat(AnimationCacheHelper.DirectionX, value.x, 0.3f, Time.deltaTime);
        }

        public void IsMovingAnimation(bool value)
        {
            _animator.SetBool(AnimationCacheHelper.IsRunning, value);
        }

        public void IsResourceCollecting(bool value)
        {
            _animator.SetBool(AnimationCacheHelper.IsAttacking, value);
        }
    }
}