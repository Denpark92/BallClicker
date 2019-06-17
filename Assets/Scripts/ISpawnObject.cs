using System;

namespace Scripts
{
    public interface ISpawnObject
    {
        void SetData();
        event Action<ISpawnObject> DestroyEvent;
        void Destroy();
    }
}