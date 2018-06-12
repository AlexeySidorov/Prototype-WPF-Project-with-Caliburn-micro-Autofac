using System;
using Autofac.Util;

namespace Task2
{
    public sealed class DisposableAction : Disposable
    {
        public DisposableAction(Action action)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
        }

        private Action Action { get; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                Action.Invoke();
            }
        }
    }
}
