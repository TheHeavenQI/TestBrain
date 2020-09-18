using System;
using System.Collections;
using UnityEngine;

namespace BaseFramework {
    public static class CoroutineTaskExtension {

        public static CoroutineTask Delay(this MonoBehaviour self, IEnumerator enumerator) {
            return TaskHelper.Create<CoroutineTask>()
                .SetMonoBehaviour(self)
                .Delay(enumerator)
                .Name($"{self.name}_enumerator");
        }

        public static CoroutineTask Delay(this MonoBehaviour self, YieldInstruction yieldInstruction) {
            return TaskHelper.Create<CoroutineTask>()
                .SetMonoBehaviour(self)
                .Delay(yieldInstruction)
                .Name($"{self.name}_yieldInstruction");
        }

        public static CoroutineTask Delay(this MonoBehaviour self, float seconds) {
            return TaskHelper.Create<CoroutineTask>()
                .SetMonoBehaviour(self)
                .Delay(seconds)
                .Name($"{self.name}_delay_{seconds}");
        }

        public static CoroutineTask Delay(this MonoBehaviour self, Func<bool> waitUntil) {
            return TaskHelper.Create<CoroutineTask>()
                .SetMonoBehaviour(self)
                .Delay(waitUntil)
                .Name($"{self.name}_waitUntil");
        }

        public static CoroutineTask CreatCoroutineTask(this MonoBehaviour self) {
            return TaskHelper.Create<CoroutineTask>()
                .SetMonoBehaviour(self)
                .Name(self.name);
        }
    }
}
