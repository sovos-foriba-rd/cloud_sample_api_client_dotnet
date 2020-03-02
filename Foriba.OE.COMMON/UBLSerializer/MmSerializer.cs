using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foriba.OE.COMMON.UBLSerializer
{
    public class MmSerializer :UBLBaseSerializer
    {
        public MmSerializer()
        {
            SerializerNamespace.Add("", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
        }
    }
}
