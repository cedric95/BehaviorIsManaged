

// Generated on 10/25/2012 10:42:49
using System;
using System.Collections.Generic;
using System.Linq;
using BiM.Protocol.Types;
using BiM.Core.IO;
using BiM.Core.Network;

namespace BiM.Protocol.Messages
{
    public class ExchangeErrorMessage : NetworkMessage
    {
        public const uint Id = 5513;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public sbyte errorType;
        
        public ExchangeErrorMessage()
        {
        }
        
        public ExchangeErrorMessage(sbyte errorType)
        {
            this.errorType = errorType;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(errorType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            errorType = reader.ReadSByte();
        }
        
    }
    
}