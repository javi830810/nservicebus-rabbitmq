using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Message.Event;
using NServiceBus;

using JotForm;

namespace ScaleBridge.Core
{
	public interface IBitrixMessageTransformManager
    {
		void Transform(BitrixMessage message);
    }

	public class BitrixMessageTransformManager:IBitrixMessageTransformManager
    {
        //@bweller
        //Injected Bus!!!
        public IBus Bus { get; set; }

		public void Transform(BitrixMessage message)
        {
			var newMessage = new BitrixApiMessage ();

			Console.WriteLine ("BitrixMessage received.");

//			lead_args = {
//				'LOGIN': config.get('BITRIX_APP_LOGIN'),
//				'PASSWORD': config.get('BITRIX_APP_PASSWORD'),
//				'ASSIGNED_BY_ID': config.get('BITRIX_ASSIGNED_BY_ID', 2),
//				'SOURCE_ID': "WEB"
//			}
//
//				key_mappings = {
//				"userType": "UF_CRM_1428691203",
//				"emailAddress": "EMAIL_WORK",
//				"challengeTitle": ["TITLE", "UF_CRM_1428691018"],
//				"phoneNumber.full": "PHONE_WORK",
//				"fullName.first": "NAME",
//				"fullName.last": "LAST_NAME",
//				"challengeDescription": "UF_CRM_1428691156",
//				"fullcontactProfileData.websites[0]": "WEB_WORK",
//				"fullcontactProfileData.social.Facebook": "WEB_FACEBOOK",
//				"fullcontactProfileData.social.Twitter": "WEB_TWITTER",
//				"fullcontactProfileData.social.LinkedIn": "WEB_OTHER",
//				"fullcontactProfileData.chats.skype": "IM_SKYPE",
//				"fullcontactProfileData.chats.gtalk": "IM_JABBER",
//				"fullcontactProfileData.organizations[0].name": "COMPANY_TITLE",
//				"fullcontactProfileData.organizations[0].title": "POST"
//			}
        }
    }
}
