using System;
using System.Collections.Generic;
using DBDL.CommonDLL;
using ServerConsole.CommonClientGameServer.Handlers;


namespace GameServer {
    public class ServerUpdateStateSM : BaseServerMessage {
        public RWDictionary<RWInt, RWBasePlayerUpdateState> Players =
            new RWDictionary<RWInt, RWBasePlayerUpdateState>();

        public RWDictionary<RWInt, CageStaticObjectState> Cages =
            new RWDictionary<RWInt, CageStaticObjectState>();

        public RWDictionary<RWInt, CampFireStaticObjectState> CampFires =
            new RWDictionary<RWInt, CampFireStaticObjectState>();

        public RWDictionary<RWInt, GateStaticObjectState> Gates =
            new RWDictionary<RWInt, GateStaticObjectState>();

        public HatchStaticObjectState Hatch = new HatchStaticObjectState();
        public RWServerCommandsQueue ServerCommandsQueue = new RWServerCommandsQueue(15);
        public RWAbilitiesList AbilitiesList = new RWAbilitiesList();

        public int LastHandledActionId = -1;

        public long CurrentServerTimestamp;

        public ServerUpdateStateSM() : base() { }
        public ServerUpdateStateSM(BinaryStreamReader reader) : base(reader) { }

      /*  public ServerUpdateStateSM(EGameMessageTypeFromServer messageTypeFromServer, BinaryStreamReader reader) : base(
            messageTypeFromServer, reader) { }*/

        public ServerUpdateStateSM(
            RWDictionary<RWInt, RWBasePlayerUpdateState> players,
            RWDictionary<RWInt, CageStaticObjectState> cages,
            RWDictionary<RWInt, CampFireStaticObjectState> campFires,
            RWDictionary<RWInt, GateStaticObjectState> gates,
            HatchStaticObjectState hatch,
            RWServerCommandsQueue commandsQueue, RWAbilitiesList abilitiesList, long currentServerTimestamp,
            int lastHandledActionId) {
            
            Players = players;
            Cages = cages;
            CampFires = campFires;
            Gates = gates;
            Hatch = hatch;
            ServerCommandsQueue = commandsQueue;
            AbilitiesList = abilitiesList;
            CurrentServerTimestamp = currentServerTimestamp;
            LastHandledActionId = lastHandledActionId;
        }

        public override void Write(BinaryStreamWriter writer) {
            Players.Write(writer);
            Cages.Write(writer);
            CampFires.Write(writer);
            Gates.Write(writer);
            Hatch.Write(writer);
            ServerCommandsQueue.Write(writer);
            writer.Write(CurrentServerTimestamp);
            writer.Write(LastHandledActionId);
        }

        public override void FillsFromReader(BinaryStreamReader reader) {
            Players.FillsFromReader(reader, RWInt.Build, RWBasePlayerUpdateState.Build);
            Cages.FillsFromReader(reader, RWInt.Build, CageStaticObjectState.Build);
            CampFires.FillsFromReader(reader, RWInt.Build, CampFireStaticObjectState.Build);
            Gates.FillsFromReader(reader, RWInt.Build, GateStaticObjectState.Build);
            Hatch.FillsFromReader(reader);
            ServerCommandsQueue.FillsFromReader(reader);
            CurrentServerTimestamp = reader.ReadInt64();
            LastHandledActionId = reader.ReadInt();
        }

        public override void Accept(BaseGameServerMessagesHandler handler) {
            handler.Handle(this);
        }

        /*protected override EGameMessageTypeFromServer GetMessageTypeFromServer() {
            return EGameMessageTypeFromServer.ServerUpdateState;
        }*/
    }
}