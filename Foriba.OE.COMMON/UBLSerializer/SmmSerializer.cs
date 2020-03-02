using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foriba.OE.COMMON.UBLSerializer
{
    public class SmmSerializer : UBLBaseSerializer
    {
        public SmmSerializer()
        {
            SerializerNamespace.Add("q1", "http://earsiv.efatura.gov.tr");
        }
    }
}
