using System;

namespace BaseFramework
{

    public interface IPool<T> where T : class
    {
        T Pop();

        bool Push(T item);
    }
}