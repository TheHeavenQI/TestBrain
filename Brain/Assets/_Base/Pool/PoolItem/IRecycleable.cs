using System;

namespace BaseFramework
{
    public interface IRecycleable
    {
        bool isRecycled { get; set; }

        /// <summary>
        /// Call only when created 
        /// </summary>
        void OnShow();
        /// <summary>
        /// Call when you recycle
        /// </summary>
        void OnRecycle();

        void ReturnToPool();

        void Destory();
    }
}