using System;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.UI.Loader
{
    public class LoaderBinder : IInitializable, IDisposable
    {
        private readonly LoaderView _loaderView;
        private readonly LoaderViewModel _loaderViewModel;

        public LoaderBinder(LoaderView loaderView, LoaderViewModel loaderViewModel)
        {
            _loaderView = loaderView;
            _loaderViewModel = loaderViewModel;
        }

        public void Initialize()
        {
            _loaderViewModel.OnShowPopup += Show;

            Bind();
        }

        private void Bind()
        {
            _loaderViewModel.LocalScoreText.Subscribe(x =>
                _loaderView.SetText(x, _loaderView.LocalSaveGroup.ScoreDataText))
                .AddTo(_loaderView);

            _loaderViewModel.LocalDataTime.Subscribe(x =>
                _loaderView.SetText(x.ToString(), _loaderView.LocalSaveGroup.TimeDataText))
                .AddTo(_loaderView);

            _loaderViewModel.CloudDataTime
                .Subscribe(x => _loaderView.SetText(x.ToString(), _loaderView.CloudSaveGroup.TimeDataText))
                .AddTo(_loaderView);

            _loaderViewModel.CloudScoreText.Subscribe(x =>
                _loaderView.SetText(x, _loaderView.CloudSaveGroup.ScoreDataText)).AddTo(_loaderView);

            _loaderView.CloudSaveGroup.LoadButton.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _loaderViewModel.ChoiceCloudSaveCommand.Execute(x);
                    Hide();
                }).AddTo(_loaderView);

            _loaderView.LocalSaveGroup.LoadButton.OnClickAsObservable()
                .Subscribe(x =>
                {
                    _loaderViewModel.ChoiceLocalSaveCommand.Execute(x);
                    Hide();
                }).AddTo(_loaderView);
        }

        public void Dispose()
        {
            _loaderViewModel.OnShowPopup -= Show;
        }

        private void Show()
        {
            _loaderView.Show();
        }

        private void Hide()
        {
            _loaderView.Hide();
        }
    }
}