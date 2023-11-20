using UnityEngine;

namespace HappyHourRts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Resource Data Container", menuName = "Happy Hour/Data Container/Resource Data Container")]
    public class ResourceDataContainerSO : ScriptableObject
    {
        [SerializeField] int _minValue;
        [SerializeField] int _maxValue;

        public int RandomValue => Random.Range(_minValue, _maxValue);
    }
}