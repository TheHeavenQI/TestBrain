using UnityEngine;

namespace BaseFramework {
    public abstract class MonoRecycleItem<T> : MonoRecycleItem where T : MonoRecycleItem {
        public override void ReturnToPool() {
            MonoPool<T>.instance.Push(this as T);
        }
    }

    public abstract class MonoRecycleItem : MonoBehaviour, IRecycleable {
        public virtual bool isRecycled { get; set; }

        public virtual void OnShow() {

        }

        public virtual void OnRecycle() {
            this.Inactive();
        }

        public virtual void ReturnToPool() {
            MonoPool<MonoRecycleItem>.instance.Push(this);
        }

        public virtual void Destory() {
            Object.Destroy(this.gameObject);
        }
    }
}
