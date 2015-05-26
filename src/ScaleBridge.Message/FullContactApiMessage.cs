using System;
using System.Collections.Generic;

namespace ScaleBridge.Message
{
	public class chatType
	{
		public string client {
			get;
			set;
		}
		public string handle {
			get;
			set;
		}
	};

	public class websiteType {
		public string url {
			get;
			set;
		}
	};

	public class organizationType {
		public bool isPrimary {
			get;
			set;
		}
		public string name {
			get;
			set;
		}
		public string startDate {
			get;
			set;
		}
		public string title {
			get;
			set;
		}
		public bool current {
			get;
			set;
		}
	};

	public class socialProfileType {
		public string type {
			get;
			set;
		}
		public string typeId {
			get;
			set;
		}
		public string typeName {
			get;
			set;
		}
		public string url {
			get;
			set;
		}
		public string bio {
			get;
			set;
		}
		public string id {
			get;
			set;
		}
		public string username {
			get;
			set;
		}
		public int followers {
			get;
			set;
		}
		public int following {
			get;
			set;
		}
	};

	public class contactInfoType {
		public List<chatType> chats { get; set; }
		public List<websiteType> websites { get; set; }
		public string familyName { get; set; }
		public string fullName { get; set; }
		public string givenName { get; set; }
	};

	public class FullContactApiMessage { 
		public string status {
			get;
			set;
		}
		public decimal likelihood {
			get;
			set;
		}
		public contactInfoType contactInfo {
			get;
			set;
		}
		public List<organizationType> organizations { get; set; }
		public List<socialProfileType> socialProfiles { get; set; }
	};

}

