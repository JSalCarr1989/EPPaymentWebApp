﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IResponsePaymentRepository
    {
        int CreateResponsePayment(ResponsePaymentDTO responseDTO);
    }
}
