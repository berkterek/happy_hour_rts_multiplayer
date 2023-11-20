using UnityEngine;

namespace HappyHourRts.Abstracts.Controllers
{
    public interface IResourceController
    {
        void SoldierCollectingResource(int value);
        Transform Target { get; }
        Transform transform { get; }
        bool CanDestroy { get; } 
    }
}