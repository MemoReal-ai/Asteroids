using System;
using MVVM;
using R3;

namespace SampleGame.UI
{
    public sealed class ViewSetterBinder<T> : IBinder, IDisposable
    {
        private readonly Action<T> _view;
        private readonly ReactiveProperty<T> _property;
        private IDisposable _handle;

        public ViewSetterBinder(Action<T> view, ReactiveProperty<T> property)
        {
            this._view = view;
            this._property = property;
        }

        public void Bind()
        {
            this._view.Invoke(this._property.Value);

            _handle = this._property.Subscribe(value => this._view.Invoke(value));
        }

        public void Unbind()
        {
            Dispose();
        }

        public void Dispose()
        {
            _handle?.Dispose();
            _handle = null;
        }
    }
}
