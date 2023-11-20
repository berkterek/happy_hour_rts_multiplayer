using UnityEngine;

namespace HappyHourRts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Wallet", menuName = "Happy Hour/Players/Player Wallet")]
    public class PlayerWallet : ScriptableObject
    {
        [SerializeField] int _wood;
        
        public event System.Action<int> OnWoodIncreased;

#if UNITY_EDITOR
        void OnEnable()
        {
            _wood = 0;
        }
#endif

        public void IncreaseWood(int wood)
        {
            _wood += wood;
            OnWoodIncreased?.Invoke(_wood);
        }
    }
}