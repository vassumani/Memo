using System;

namespace memo.Models
{
	private int ModelNumber = 1;
	
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}