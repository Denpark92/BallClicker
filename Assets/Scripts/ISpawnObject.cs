using System;

namespace Scripts
{
    public interface ISpawnObject
    {
        void SetSpawned();
        event Action<ISpawnObject> DestroyEvent;
    }
}