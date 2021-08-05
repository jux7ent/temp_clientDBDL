using System;
using System.Threading;



namespace GameServer {
    public abstract class BaseAbilitiesHandler {
        public abstract bool Handle(InvisAbility invisAbility);
        public abstract bool Handle(SpawnTrapAbility spawnTrapAbility);


        protected void InvokeAfterTime(int timeInMills, Action action) {
            new Thread(() => {
                Thread.Sleep(timeInMills);
                action();
            }).Start();
        }
    }
}