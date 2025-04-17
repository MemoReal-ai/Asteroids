using System;
using MVVM;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGame.UI
{
    public sealed class ImageBinder : IBinder, IObserver<Sprite>
    {
        private readonly Image _view;
        private readonly ReactiveProperty<Sprite> _property;
        private IDisposable _handle;

        public ImageBinder(Image view, ReactiveProperty<Sprite> property)
        {
            this._view = view;
            this._property = property;
        }

        public void Bind()
        {
            this.OnNext(this._property.Value);
            _handle = this._property.Subscribe();
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnNext(Sprite value)
        {
            this._view.sprite = value;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
    }
}