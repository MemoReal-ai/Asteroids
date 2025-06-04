using System;
using TMPro;
using UnityEngine;

namespace _Game.MainMenu.Logic.UI
{
    public class LoaderView : MonoBehaviour
    {
        public UISaveGroup CloudSaveGroup;
        public UISaveGroup LocalSaveGroup;


        private void Start()
        {
            Hide();
        }

        public void SetText(string text, TextMeshProUGUI textMesh)
        {
            textMesh.text = text;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}