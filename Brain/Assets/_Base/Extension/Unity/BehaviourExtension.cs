using UnityEngine;

namespace BaseFramework
{
    public static class BehaviourExtension
    {
        public static T Enable<T>(this T self) where T : Behaviour
        {
            if (self)
            {
                self.enabled = true;
            }
            return self;
        }

        public static T Disable<T>(this T self) where T : Behaviour
        {
            if (self)
            {
                self.enabled = false;
            }
            return self;
        }

        public static void EnableBehaviour<K>(this Behaviour self, bool isEnable) where K : Behaviour
        {
            if (!self || !self.gameObject) return;

            K k = self.GetComponent<K>();
            if (!k)
            {
                return;
            }
            k.enabled = isEnable;
        }
    }
}
