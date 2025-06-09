using System;
using R3;
using UnityEngine;
using Zenject;

namespace _Game.MainMenu.Logic.UI
{
    public class BinderViewScore : IInitializable
    {
        private readonly ViewScore _viewScore;
        private readonly ViewScoreModelView _viewScoreModel;

        public BinderViewScore(ViewScore viewScore, ViewScoreModelView viewScoreModel)
        {
            _viewScore = viewScore;
            _viewScoreModel = viewScoreModel;
        }

        public void Initialize()
        {
            _viewScoreModel.HighScore
                .Subscribe(score => _viewScore.SetHighScore(score))
                .AddTo(_viewScore);


            _viewScoreModel.ScoreLastSession
                .Subscribe(score => _viewScore.SetScoreLastSession(score))
                .AddTo(_viewScore);
        }
    }
}