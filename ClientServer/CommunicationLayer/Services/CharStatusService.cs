using CharStatus;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLayer.Services
{
    public class CharStatusService: CharStatus.CharStatus.CharStatusClient
    {
        public override MoveAck Move(CharStatus.Status request, CallOptions options)
        {
            return base.Move(request, options);
        }

        public override AsyncUnaryCall<MoveAck> MoveAsync(CharStatus.Status request, CallOptions options)
        {
            return base.MoveAsync(request, options);
        }

    }
}
