namespace HappyHourRts.Abstracts.Animations
{
    public interface ISoldierAnimationService
    {
        void Tick();
        void LateTick();
        void IsResourceCollecting(bool value);
    }
}