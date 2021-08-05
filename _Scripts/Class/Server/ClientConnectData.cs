namespace Server {
    namespace ServerData {
        public class ClientConnectData {
            public ECharacter Character;
            public bool IsSurvivor;

            public ClientConnectData(ECharacter character, bool isSurvivor) {
                Character = character;
                IsSurvivor = isSurvivor;
            }
        }
    }
}