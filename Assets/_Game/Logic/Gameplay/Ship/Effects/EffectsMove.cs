using System.Collections.Generic;
using UnityEngine;

namespace _Game.Gameplay.Logic.Ship.Effects
{
    public class EffectsMove : MonoBehaviour
    {
        [SerializeField] private Animator[] _animators;

        private readonly int _isMoving = Animator.StringToHash("IsMoving");

        public void SetTriggerMoveAnimation(bool isMove)
        {
            foreach (Animator animator in _animators)
            {
                animator.SetBool(_isMoving, isMove);
            }
        }
        // не понимаю почему не работает!!
    }
}