using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Enums
{

    public enum RequestStatusEnum : byte
    {
        InProgress = 1,
        WaitingForCorrection = 2,
        Approved = 3,
        WaitingForBL = 4,
        WaitingForPaymnet = 5,
        WaitingForOriginalBL = 6,
        Closed = 7
    }
}
