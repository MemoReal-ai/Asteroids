using System.Collections.Generic;
using UnityEngine;

namespace _Game.Gameplay.Logic.Service.ObjectPool
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly int _size;
        private readonly Transform _container;
        private readonly bool _autoExpand;
        private List<T> _objects;

        public IEnumerable<T> Objects => _objects;

        public ObjectPool(T prefab, int size, Transform container, bool autoExpand)
        {
            _prefab = prefab;
            _size = size;
            _container = container;
            _autoExpand = autoExpand;

            CreatePool();
        }

        private void CreatePool()
        {
            _objects = new List<T>();
            for (int i = 0; i < _size; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActive = false)
        {
            T obj = Object.Instantiate(_prefab, _container);
            obj.gameObject.SetActive(isActive);
            _objects.Add(obj);
            return obj;
        }

        public T GetObject()
        {
            if (HasFreeObject(out T obj))
            {
                obj.gameObject.SetActive(true);
                return obj;
            }

            if (_autoExpand)
            {
                CreateObject(true);
            }

            return null;
        }

        private bool HasFreeObject(out T monoObj)
        {
            foreach (var obj in _objects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    monoObj = obj;
                    return true;
                }
            }

            monoObj = null;
            return false;
        }
    }
}