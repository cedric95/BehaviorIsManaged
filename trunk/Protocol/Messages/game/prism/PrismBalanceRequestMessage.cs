

// Generated on 10/25/2012 10:42:53
using System;
using System.Collections.Generic;
using System.Linq;
using BiM.Protocol.Types;
using BiM.Core.IO;
using BiM.Core.Network;

namespace BiM.Protocol.Messages
{
    public class PrismBalanceRequestMessage : NetworkMessage
    {
        public const uint Id = 5839;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        
        public PrismBalanceRequestMessage()
        {
        }
        
        
        public override void Serialize(IDataWriter writer)
        {
        }
        
        public override void Deserialize(IDataReader reader)
        {
        }
        
    }
    
}