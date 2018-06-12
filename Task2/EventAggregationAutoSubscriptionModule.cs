using Autofac;
using Autofac.Core;
using Caliburn.Micro;

namespace Task2
{
    public class EventAggregationAutoSubscriptionModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry registry, IComponentRegistration registration)
        {
            registration.Activated += OnComponentActivated;
        }

        static void OnComponentActivated(object sender, ActivatedEventArgs<object> args)
        {
            var handler = args?.Instance as IHandle;
            if (handler == null)
            {
                return;
            }

            var context = args.Context;
            var lifetimeScope = context.Resolve<ILifetimeScope>();
            var eventAggregator = lifetimeScope.Resolve<IEventAggregator>();

            eventAggregator.Subscribe(handler);

            var disposableAction = new DisposableAction(() =>
            {
                eventAggregator.Unsubscribe(handler);
            });

            lifetimeScope.Disposer.AddInstanceForDisposal(disposableAction);
        }
    }
}
