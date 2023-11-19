using UnityEngine;

namespace HappyHourRts.Abstracts.Controllers
{
    public interface IClickableController
    {
        void Select();
        void Unselect();
        void SetTarget(Vector3 position);
    }
}