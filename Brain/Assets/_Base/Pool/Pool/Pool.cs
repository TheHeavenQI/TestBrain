using System;
using System.Collections.Generic;

namespace BaseFramework
{
    public class Pool<T> : IPool<T> where T : class, IRecycleable
    {
        protected Stack<T> cacheStack;
        protected ICreator<T> creator;
        protected int maxPoolSize;

        public Pool<T> Init(int initPoolSize = -1, int maxPoolSize = -1)
        {
            if (creator != null) return this;
            creator = new SimpleCreator<T>();
            InitPoolSize(initPoolSize, maxPoolSize);
            return this;
        }

        public Pool<T> Init(Func<T> createFunc, int initPoolSize = -1, int maxPoolSize = -1)
        {
            if (creator == null)
            {
                creator = new SimpleCreator<T>(createFunc);
                InitPoolSize(initPoolSize, maxPoolSize);
            }
            return this;
        }

        protected void InitPoolSize(int initPoolSize, int maxPoolSize)
        {
            this.maxPoolSize = maxPoolSize;
            cacheStack = initPoolSize <= 0 ? new Stack<T>() : new Stack<T>(initPoolSize);
        }

        public T Pop()
        {
            if (creator == null)
                Init();

            T item = CreateFromCache();
            if (!CheckUseful(item))
            {
                item = creator.Create();
            }

            item.OnShow();
            item.isRecycled = false;

            OnItemCreate(item);

            return item;
        }

        private T CreateFromCache()
        {
            T result = null;
            while (cacheStack.Count > 0)
            {
                result = cacheStack.Pop();
                if (CheckUseful(result))
                    break;
                Log.W(this, "{0} pool have unusefulness data!", typeof(T).Name);
            }
            return result;
        }

        protected virtual bool CheckUseful(T t)
        {
            return (t as T) != null;
        }

        public bool Push(T item)
        {
            if (item == null || item.isRecycled)
                return false;

            OnItemRecycle(item);

            if (maxPoolSize > 0 && cacheStack.Count >= maxPoolSize)
            {
                item.OnRecycle();
                return false;
            }

            item.isRecycled = true;
            item.OnRecycle();
            cacheStack.Push(item);

            return true;
        }

        protected virtual void OnItemCreate(T item)
        {

        }

        protected virtual void OnItemRecycle(T item)
        {

        }

        public virtual void Destory()
        {
            cacheStack.ForEach(it => it.Destory());

            cacheStack = null;
            creator = null;
        }
    }
}
