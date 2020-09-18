using System;

namespace BaseFramework
{
    public interface ISingleton
    {
        void OnSingletonInit();

        void OnSingletonDestroy();
    }
}