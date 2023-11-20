using HappyHourRts.Abstracts.Controllers;
using UnityEngine;

namespace HappyHourRts.Abstracts.Animations
{
    public interface ISoldierAnimationDal
    {
        void DirectionSetterAnimation(Vector3 value);
        void IsMovingAnimation(bool value);
        void IsResourceCollecting(bool value);
        public bool IsResourceCollectingNow { get; }
        public void SetClickableController(IClickableController clickableController);
    }
}