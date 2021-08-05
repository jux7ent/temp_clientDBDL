using DBDL.CommonDLL;

namespace GameServer {
    public class LobbyPlayerInfo : BaseReadableWritable {
        public int PlayerId;
        public string Name;
        public ECharacter CharacterType;
        public bool ReadyState = false;
        
        public LobbyPlayerInfo() {}

        public LobbyPlayerInfo(int playerId, string name) {
            PlayerId = playerId;
            Name = name;
        }

        public LobbyPlayerInfo(int playerId, string name, ECharacter characterType, bool readyState) : this(playerId, name) {
            CharacterType = characterType;
            ReadyState = readyState;
        }

        public LobbyPlayerInfo(int playerId, string name, LobbyInfoCM lobbyInfoCm) : this(playerId, name) {
            Update(lobbyInfoCm);
        }

        public void Update(LobbyInfoCM lobbyInfoCm) {
            CharacterType = lobbyInfoCm.CharacterType;
            ReadyState = lobbyInfoCm.ReadyState;
        }

        public LobbyPlayerInfo(BinaryStreamReader reader) : base(reader) { }

        public override void FillsFromReader(BinaryStreamReader reader) {
            PlayerId = reader.ReadInt32();
            Name = reader.ReadString();
            CharacterType = (ECharacter) reader.ReadInt32();
            ReadyState = reader.ReadBoolean();
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(PlayerId);
            writer.Write(Name);
            writer.Write((int) CharacterType);
            writer.Write(ReadyState);
        }
    }
}