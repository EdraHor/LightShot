using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Pool
{
    class EnemyPooling : GameObjectsPooling
    {
        public EnemyPooling(GameObject prefab, int startCount, Transform container, bool isAutoExpand) : base
        (prefab, startCount, container, isAutoExpand)
        {

        }

        public void Spawn(Vector3 position)
        {
            var enemy = GetFreeElement();
            enemy.transform.position = position;
        }
    }
}
