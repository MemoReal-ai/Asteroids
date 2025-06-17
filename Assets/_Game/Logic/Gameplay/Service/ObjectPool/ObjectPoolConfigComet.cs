using _Game.Gameplay.Logic.Enemy;
using _Game.Logic.Gameplay.Enemy;
using UnityEngine;

namespace _Game.Gameplay.Logic.Service.ObjectPool
{
    [CreateAssetMenu(fileName = "ObjectPoolConfigEnemy",menuName = "Config/ObjectPoolConfig/Enemy/Create")]
    public class ObjectPoolConfigEnemy : ObjectPoolConfig<EnemyAbstract>
    {
    
    }
}
