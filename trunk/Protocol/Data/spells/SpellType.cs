

// Generated on 10/25/2012 10:43:02
using System;
using System.Collections.Generic;
using BiM.Protocol.Tools;

namespace BiM.Protocol.Data
{
    [D2OClass("SpellTypes")]
    public class SpellType : IDataObject
    {
        private const String MODULE = "SpellTypes";
        public int id;
        public uint longNameId;
        public uint shortNameId;
    }
}