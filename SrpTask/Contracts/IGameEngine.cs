using SrpTask.Contracts;
using System.Collections.Generic;

namespace SrpTask.Contracts
{
    public interface IGameEngine
    {
        void PlaySpecialEffect(string effectName);
        List<IEnemy> GetEnemiesNear(RpgPlayer player);
    }
}