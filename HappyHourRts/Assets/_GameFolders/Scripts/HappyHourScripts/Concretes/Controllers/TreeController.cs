using HappyHourRts.Abstracts.Controllers;
using HappyHourRts.ScriptableObjects;
using UnityEngine;

namespace HappyHourRts.Controllers
{
    public class TreeController : MonoBehaviour,IResourceController
    {
        [SerializeField] PlayerWallet _playerWallet;
        [SerializeField] ResourceDataContainerSO _resourceDataContainer;
        [SerializeField] Transform _target;
        [SerializeField] int _maxResourceValue = 0;

        int _currentResourceValue;
        
        public Transform Target => _target;
        public bool CanDestroy => _currentResourceValue <= 0;
        
        void Start()
        {
            _maxResourceValue = _resourceDataContainer.RandomValue;
            _currentResourceValue = _maxResourceValue;
        }

        public void SoldierCollectingResource(int value)
        {
            int result = _currentResourceValue - value;

            _currentResourceValue = result;
            _playerWallet.IncreaseWood(result);
        }
    }    
}