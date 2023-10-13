using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ViewModel.ResponseModel
{
    public class BaseResponseModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
    }
}
