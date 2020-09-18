namespace BaseFramework
{
    public abstract class SimpleRecycleItem<T> : IRecycleable where T : class, IRecycleable
    {
        public bool isRecycled { get; set; }

        public virtual void OnShow()
        {
        }

        public virtual void OnRecycle()
        {
        }

        public virtual void ReturnToPool() {
            SimplePool<T>.instance.Push(this as T);
        }

        public virtual void Destory() {

        }
    }
}
