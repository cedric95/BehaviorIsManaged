

// Generated on 10/25/2012 10:42:31
using System;
using System.Collections.Generic;
using System.Linq;
using BiM.Protocol.Types;
using BiM.Core.IO;
using BiM.Core.Network;

namespace BiM.Protocol.Messages
{
    public class AdminCommandMessage : NetworkMessage
    {
        public const uint Id = 76;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string content;
        
        public AdminCommandMessage()
        {
        }
        
        public AdminCommandMessage(string content)
        {
            this.content = content;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(content);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            content = reader.ReadUTF();
        }
        
    }
    
}