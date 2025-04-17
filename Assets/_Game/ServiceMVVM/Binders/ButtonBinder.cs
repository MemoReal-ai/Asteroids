using System;
using MVVM;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.ServiceMVVM.Binders
{
    public sealed class ButtonBinder : IBinder
    {
        private readonly Button _view;
        private readonly UnityAction _modelAction;

        public ButtonBinder(Button view, Action model)
        {
            this._view = view;
            this._modelAction = new UnityAction(model);
        }

        void IBinder.Bind()
        {
            this._view.onClick.AddListener(this._modelAction);
        }

        void IBinder.Unbind()
        {
            this._view.onClick.RemoveListener(this._modelAction);
        }
    }
}