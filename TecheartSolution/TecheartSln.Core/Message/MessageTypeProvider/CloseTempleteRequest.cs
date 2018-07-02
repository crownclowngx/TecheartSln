using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartSln.Core.Message.MessageTypeProvider
{
    public class CloseTempleteRequest:BaseMessageRequest
    {
        public String TempleteGuid { get; set; }
    }
}
