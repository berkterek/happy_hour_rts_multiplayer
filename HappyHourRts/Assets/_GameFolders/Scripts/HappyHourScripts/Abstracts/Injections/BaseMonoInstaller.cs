using Zenject;

namespace HappyHourRts.Abstracts.Injections
{
    public abstract class BaseMonoInstaller : MonoInstaller
    {
        protected abstract void BindInstance();  
        
        public override void InstallBindings()
        {
            BindInstance();
        }
    }
}