using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework {
    public static class PoolHelper {
        private const int DEFAULT_INIT_POOL_SIZE = 10;
        private const int DEFAULT_MAX_POOL_SIZE = 50;

        public static T Pop<T>(int initPoolSize = DEFAULT_INIT_POOL_SIZE,
                                  int maxPoolSize = DEFAULT_MAX_POOL_SIZE) where T : class, IRecycleable
        {
            return SimplePool<T>.instance.Init(initPoolSize, maxPoolSize).Pop();
        }

        public static T Pop<T>(Func<T> createFunc,
                                  int initPoolSize = DEFAULT_INIT_POOL_SIZE,
                                  int maxPoolSize = DEFAULT_MAX_POOL_SIZE) where T : class, IRecycleable
            {
            return SimplePool<T>.instance.Init(createFunc, initPoolSize, maxPoolSize).Pop();
        }

        public static T Pop<T>(T prefab,
                                  int initPoolSize = DEFAULT_INIT_POOL_SIZE,
                                  int maxPoolSize = DEFAULT_MAX_POOL_SIZE) where T : MonoRecycleItem
        {
            T item = MonoPool<T>.instance
                .Pop(prefab,
                      DEFAULT_INIT_POOL_SIZE,
                      DEFAULT_MAX_POOL_SIZE);
            return item;
        }
    }
}
