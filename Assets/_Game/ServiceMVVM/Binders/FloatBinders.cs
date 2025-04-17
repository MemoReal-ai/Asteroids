using System;
using MVVM;
using R3;

namespace _Game.ServiceMVVM.Binders
{
    public class FloatBinders : IBinder
    {
        private float _value;
        private readonly ReactiveProperty<float> _property;
        private IDisposable _handle;

        public FloatBinders(float value, ReactiveProperty<float> property)
        {
            _value = value;
            _property = property;
        }

        public void Bind()
        {
            _value = _property.Value;
            _handle = _property.Subscribe(x => _value = x);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }
    }
}