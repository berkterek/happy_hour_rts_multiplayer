using UnityEngine;

namespace HappyHourRts.Abstracts.Controllers
{
    public interface IClickableController
    {
        Transform transform { get; }
        void Select();
        void Unselect();
        void SetTarget(Vector3 position);
        void SetResourceToSoldier(IResourceController resourceController);
        bool HasAuthority { get; }
    }
}