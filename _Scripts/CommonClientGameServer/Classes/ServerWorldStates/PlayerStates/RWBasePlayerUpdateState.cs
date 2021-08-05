using System;
using System.Threading;
using DBDL.CommonDLL;


namespace GameServer {
    
    public class HeaderPlayerUpdateState : BaseReadableWritable {
        public bool IsCatcher;
        public int PlayerId = -1;
        public LocationData LocationData = new LocationData();
        public BitMap CanDo = new BitMap(ECanDo.Interact, ECanDo.Move);
        public CharacterSettings CharacterSettings = new CharacterSettings();

        public void Update(HeaderPlayerUpdateState header) {
            IsCatcher = header.IsCatcher;
            PlayerId = header.PlayerId;
            LocationData.Update(header.LocationData);
            CanDo.Update(header.CanDo);
            CharacterSettings.Update(header.CharacterSettings);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            IsCatcher = reader.ReadBoolean();
            PlayerId = reader.ReadInt32();
            LocationData.FillsFromReader(reader);
            CanDo.FillsFromReader(reader);
            CharacterSettings.FillsFromReader(reader);
        }

        public override void Write(BinaryStreamWriter writer) {
            writer.Write(IsCatcher);
            writer.Write(PlayerId);
            LocationData.Write(writer);
            CanDo.Write(writer);
            CharacterSettings.Write(writer);
        }
    }
    
    
    public abstract class RWBasePlayerUpdateState : BaseReadableWritable {
        public HeaderPlayerUpdateState Header = new HeaderPlayerUpdateState();

        public bool IsCatcher => Header.IsCatcher;
        public int PlayerId => Header.PlayerId;
        public LocationData LocationData => Header.LocationData;
        public CharacterSettings CharacterSettings => Header.CharacterSettings;
        public BitMap CanDo => Header.CanDo;

        public RWBasePlayerUpdateState() {
            Header.IsCatcher = BeCatcher();
        }

        public RWBasePlayerUpdateState(int playerId) : this() {
            Header.PlayerId = playerId;
        }

        public RWBasePlayerUpdateState(HeaderPlayerUpdateState header, BinaryStreamReader reader) : this() {
            Header.Update(header);
            FillsFromReaderWithoutHeader(reader);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Header.FillsFromReader(reader);
            FillsFromReaderWithoutHeader(reader);
        }

        public abstract bool BeCatcher();

        public abstract void FillsFromReaderWithoutHeader(BinaryStreamReader reader);

        public override void Write(BinaryStreamWriter writer) {
            Header.Write(writer);
        }

        public static RWBasePlayerUpdateState Build(BinaryStreamReader reader) {
            HeaderPlayerUpdateState playerHeader = new HeaderPlayerUpdateState();
            playerHeader.FillsFromReader(reader);

            if (playerHeader.IsCatcher) {
                return new RWBaseCatcherUpdateState(playerHeader, reader);
            } else {
                return new RWBaseEscaperUpdateState(playerHeader, reader);
            }
        }
    }
}