using System;

namespace BaseFramework
{
    public interface ITask<out T>
    {
        T Execute();

        T Cancel();
    }
}