using HappyHourRts.Abstracts.Injections;
using HappyHourRts.Abstracts.Inputs;
using HappyHourRts.Inputs;

namespace HappyHourRts.Injections
{
    public class PlayerInstaller : BaseMonoInstaller
    {
        protected override void BindInstance()
        {
            Container.Bind<IInputReader>().To<NewInputReader>().AsSingle().Lazy();
        }
    }
}