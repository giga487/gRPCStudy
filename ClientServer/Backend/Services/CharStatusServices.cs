using CharStatus;
using Grpc.Core;

namespace Backend.Services
{
    public class CharStatusService : CharStatus.CharStatus.CharStatusBase
    {
        public override Task<MoveAck> Move(CharStatus.Status request, ServerCallContext context)
        {
            return base.Move(request, context);
        }
    }
}
