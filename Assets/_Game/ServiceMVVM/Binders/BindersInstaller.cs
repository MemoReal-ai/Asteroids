using _Game.ServiceMVVM.Binders;
using MVVM;
using Zenject;

namespace SampleGame.UI
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<ButtonBinder>();
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ImageBinder>();
            BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
            BinderFactory.RegisterBinder<FloatBinders>();
        }
    }
}