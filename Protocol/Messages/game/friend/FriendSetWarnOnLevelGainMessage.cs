

// Generated on 12/11/2012 19:44:23
using System;
using System.Collections.Generic;
using System.Linq;
using BiM.Protocol.Types;
using BiM.Core.IO;
using BiM.Core.Network;

namespace BiM.Protocol.Messages
{
    public class FriendSetWarnOnLevelGainMessage : NetworkMessage
    {
        public const uint Id = 6077;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public bool enable;
        
        public FriendSetWarnOnLevelGainMessage()
        {
        }
        
        public FriendSetWarnOnLevelGainMessage(bool enable)
        {
            this.enable = enable;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(enable);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            enable = reader.ReadBoolean();
        }
        
    }
    
}