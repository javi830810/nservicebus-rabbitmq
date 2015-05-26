using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScaleBridge.Message.Event;
using NServiceBus;

using JotForm;

namespace ScaleBridge.Core
{
	public interface IJotformMessageTransformManager
    {
		void Transform(JotformBaseMessage message);
    }

	public class JotformMessageTransformManager:IJotformMessageTransformManager
    {
        //@bweller
        //Injected Bus!!!
        public IBus Bus { get; set; }

		public void Transform(JotformBaseMessage message)
        {
			var client = new JotForm.APIClient ("0f2d33639109363ae35474c47cfbd9da");
			var questions = client.getFormQuestions (message.FormID);

			Dictionary<int, string> questionIdNameMap = new Dictionary<int, string> ();
			foreach (var item in questions) {
				Console.WriteLine (item.Key);
			}

			var content = questions ["content"];
			Console.WriteLine (content);

			foreach (var item in content) {
				Console.WriteLine (item);
				Console.WriteLine (item.GetType().ToString());
//				Console.WriteLine (item.Name);
//				var id = int.Parse(item.Name);
			}

//			var submission = client.getSubmission (message.SubmissionID);

//			Bus.SendLocal(new JotformMessage(){
//				FormID = message.FormID,
//				SubmissionID = message.SubmissionID,
//				FormData = new Dictionary<string,string>()
//                {
//					{"first_name","brian"},
//                    {"last_name","weller"},
//                }
//            });
        }
    }
}
