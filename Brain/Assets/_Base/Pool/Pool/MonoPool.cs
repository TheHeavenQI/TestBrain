using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace BaseFramework
{
    public class MonoPoolManager : MonoSingleton<MonoPoolManager>
    {

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();

            instance.Inactive();
        }

        public static void Register(UnityAction<Scene> action)
        {
            SceneManager.sceneUnloaded += action;
        }

        public static void UnRegister(UnityAction<Scene> action)
        {
            SceneManager.sceneUnloaded -= action;
        }
    }

    public class MonoPool<T> : ISingleton, IPool<T> where T : MonoRecycleItem {
        private GameObject _poolRootObj;

        private Dictionary<string, Pool<T>> pools = new Dictionary<string, Pool<T>>();

        #region singleton

        private static class SingletonHandler {
            /// <summary>
            /// 当一个类有静态构造函数时，它的静态成员变量不会被beforefieldinit修饰
            /// 就会确保在被引用的时候才会实例化，而不是程序启动的时候实例化
            /// </summary>
            static SingletonHandler() {
                Init();
            }

            private static void Init() {
                instance = new MonoPool<T>();
                instance.OnSingletonInit();
            }

            public static MonoPool<T> instance;
        }

        public static MonoPool<T> instance => SingletonHandler.instance;

        public virtual void OnSingletonInit() {
            _poolRootObj = new GameObject();
            _poolRootObj
                .Inactive()
                .Name($"{GetType().ReadableName()}_Singleton")
                .transform.SetParent(MonoPoolManager.instance.transform, false);
        }

        public virtual void OnSingletonDestroy() {
            _poolRootObj.Destroy();
            _poolRootObj = null;
            SingletonHandler.instance = null;
        }
        #endregion

        T IPool<T>.Pop() {
            Log.E(this, "Pop item");
            if (pools.Count > 0) {
                return pools.GetEnumerator().Current.Value.Pop();
            }
            return null;
        }

        public T Pop(T prefab, int initPoolSize = -1, int maxPoolSize = -1) {
            if (!pools.ContainsKey(prefab.name)) {
                Pool<T> pool = new Pool<T>();
                pool.Init(() => UnityEngine.Object.Instantiate<T>(prefab), initPoolSize, maxPoolSize);
                pools.Add(prefab.name, pool);
            }
            T item = pools[prefab.name].Pop();
            item.name = prefab.name;
            return item;
        }

        public bool Push(T item) {

            if (pools.ContainsKey(item.name)) {
                pools[item.name].Push(item);
            }
            OnItemRecycle(item);
            return true;
        }


        protected void OnItemRecycle(T item)
        {
            item.transform
                .Inactive()
                .Parent(_poolRootObj.transform, false);
        }

        protected bool CheckUseful(T t)
        {
            return t as MonoBehaviour != null;
        }

        public void Destory()
        {
            foreach(var pair in pools) {
                pair.Value.Destory();
            }
            pools.Clear();
            OnSingletonDestroy();
        }
    }
}
