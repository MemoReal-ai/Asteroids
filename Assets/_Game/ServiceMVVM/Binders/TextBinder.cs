using System;
using MVVM;
using TMPro;
using R3;

namespace SampleGame.UI
{
    public sealed class TextBinder : IBinder
    {
        private readonly TMP_Text _view;
        private readonly ReactiveProperty<string> _property;
        private IDisposable _handle;

        public TextBinder(TMP_Text view, ReactiveProperty<string> property)
        {
            this._view = view;
            this._property = property;
        }

        public void Bind()
        {
            this._view.text = this._property.Value;
            
            _handle = this._property.Subscribe(value =>
            {
                this._view.text = value;
            });
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }
    }
}