using UnityEngine;

namespace HappyHourRts.Abstracts.Inputs
{
    public interface IInputReader
    {
        Vector2 TouchPosition { get; }
        Vector2 DeltaPosition { get; }
        bool IsTouchDown { get; }
        bool IsTouch { get; }
    }    
}