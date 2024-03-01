using System;
using System.Collections.Generic;
using System.Text;

namespace Personnel.Model.ServiceModel
{
    public class NotificationModel
    {
        public string to { get; set; }
        public string from { get; set; }
        public string sms { get; set; }
        public string type { get; set; }
        public string channel { get; set; }
        public string api_key { get; set; }
        public Media media { get; set; }
        
    }

    public class Media
    {
        public Uri Url { get; set; }
        public string Caption { get; set; }
    }

    public class TermiiResponse
    {
        public string message_id { get; set; }
        public string message { get; set; }
        public string balance { get; set; }
        public string user { get; set; }
    }

    public class BulkNotifiaction
    {
		public string to { get; set; }
		public string from { get; set; }
		public string sms { get; set; }
		public string type { get; set; }
		public string channel { get; set; }
		public string api_key { get; set; }
	}

    public class BulkResponse
    {
        public string Code { get; set; }
		public string message_id { get; set; }
		public string message { get; set; }
		public string balance { get; set; }
		public string user { get; set; }
	}
}
