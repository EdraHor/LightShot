using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class GameObjectsPooling
    {
        public GameObject Prefab { get; protected set; }
        public bool IsAutoExpand { get; set; }
        public Transform Container { get; protected set; }

        protected List<GameObject> _pool;

        public GameObjectsPooling(GameObject prefab, int count, Transform container, bool isAutoExpand)
        {
            Prefab = prefab;
            Container = container;
            IsAutoExpand = isAutoExpand;
            _pool = new List<GameObject>();
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private GameObject CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = GameObject.Instantiate(Prefab, Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElements(bool activate, out GameObject element)
        {
            foreach (var item in _pool)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    element = item;
                    item.gameObject.SetActive(activate);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public GameObject GetFreeElement(bool activate = true)
        {
            if (HasFreeElements(activate, out var element))
            {
                return element;
            }
            
            if (IsAutoExpand)
            {
                return CreateObject(true);
            }

            throw new System.Exception($"Free element {typeof(GameObject)} is not found in pool");
        }
    }
}
