using HappyHourRts.Abstracts.Animations;
using HappyHourRts.Abstracts.Injections;
using HappyHourRts.Animations;

namespace HappyHourRts.Injections
{
    public class SoldierInstaller : BaseMonoInstaller
    {
        protected override void BindInstance()
        {
            Container.Bind<ISoldierAnimationService>().To<SoldierAnimationAiPathManager>().AsTransient().Lazy();
            Container.Bind<ISoldierAnimationDal>().To<SoldierAnimationDal>().AsTransient().Lazy();
        }
    }
}