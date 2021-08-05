namespace GameServer {
    public static class Timeouts {
        public static int VeryRareMessage = 500;
        public static int RareMessage = 300;
        public static int OftenMessage = 100;

        public static int Sec2NanoSec = 10000000;
        public static int HowOftenAddNewBotMinNanoSec = 1 * Sec2NanoSec;
        public static int HowOftenAddNewBotMaxNanoSec = 2 * Sec2NanoSec;
        public static int AfterWhichTimeStartBotSpawnerNanoSec = 2 * Sec2NanoSec;

        public static int DisconnectTimeout = 8 * Sec2NanoSec;

        public static int WaitBeforeStartInitializationMills = 5000;
        
        public const int ServerUpdateTickMills = 100;
    }
}