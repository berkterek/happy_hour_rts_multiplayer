using HappyHourRts.Abstracts.Animations;
using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.Helpers;
using Pathfinding;
using UnityEngine;

namespace HappyHourRts.Animations
{
    public class SoldierAnimationAiPathManager : ISoldierAnimationService
    {
        readonly ISoldierAnimationDal _animationDal;
        AIPath _aiPath;

        Vector3 _velocity;

        public SoldierAnimationAiPathManager(ISoldierAnimationDal animationDal)
        {
            _animationDal = animationDal;
        }

        public void Tick()
        {
            _velocity = _aiPath.velocity;
        }

        public void LateTick()
        {
            if (_velocity != DirectionCacheHelper.Vector3Zero)
            {
                _animationDal.DirectionSetterAnimation(_velocity);
            }

            _animationDal.IsMovingAnimation(_velocity.magnitude > 0f);
        }

        public void IsResourceCollecting(bool value)
        {
            if (value == _animationDal.IsResourceCollectingNow) return;

            _animationDal.IsResourceCollecting(value);
        }

        public void SetClickableController(IClickableController clickableController)
        {
            _animationDal.SetClickableController(clickableController);
            _aiPath = clickableController.transform.GetComponent<AIPath>();
        }
    }
}