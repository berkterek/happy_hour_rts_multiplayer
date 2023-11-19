using UnityEngine;

namespace HappyHourRts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int _targetFps = 30; 
        
        void Awake()
        {
            Application.targetFrameRate = _targetFps;
        }
    }    
}