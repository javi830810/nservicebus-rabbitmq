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
			var newMessage = new JotformMessage () {
				FormID = message.FormID,
				SubmissionID = message.SubmissionID,
				FormData = new Dictionary<string, string> ()
			};

			var client = new JotForm.APIClient ("0f2d33639109363ae35474c47cfbd9da", true);
			var questions = client.getFormQuestions (message.FormID);

			Dictionary<int, string> questionIdNameMap = new Dictionary<int, string> ();

			var content = questions ["content"].Children();

			foreach (Newtonsoft.Json.Linq.JProperty item in content) {
				var id = int.Parse(item.Name);
				string name = (string)item.Value["name"];
				questionIdNameMap [id] = name;
			}

			var submission = client.getSubmission (message.SubmissionID);
			var answers = submission["content"]["answers"].Children();
			foreach (Newtonsoft.Json.Linq.JProperty answer in answers) {
				var qid = int.Parse(answer.Name);
				var answerValue = answer.Value["answer"];

				// TODO: How to handle nested JSON data?
				if (answerValue is Newtonsoft.Json.Linq.JObject) {
					foreach (Newtonsoft.Json.Linq.JProperty prop in answerValue) {
						var questionName = string.Join (".", new List<string> () { questionIdNameMap [qid], prop.Name });
						Console.WriteLine (string.Format ("{0}: {1}", questionName, prop.ToObject<string> ()));
						newMessage.FormData [questionName] = prop.ToObject<string> ();
					}
				}
				else {
					Console.WriteLine (string.Format ("{0}: {1}", questionIdNameMap [qid], answerValue));
					newMessage.FormData [questionIdNameMap [qid]] = answerValue.ToString ();
				}
			}
			Console.WriteLine (newMessage);

			Bus.SendLocal(newMessage);
        }
    }
}
