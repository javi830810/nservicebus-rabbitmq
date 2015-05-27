using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScaleBridge.Core;
using ScaleBridge.Message.Event;
using ScaleBridge.Message.Object;
using JF = JotForm;

namespace ScaleBridge.Partner.JotForm
{
	public class JotFormAPIAction: ITransformAction
	{
		public MessageTemplate OutputMessageTemplate { get; set; }

		public EventMessage Transform (EventMessage input)
		{
			var client = new JF.APIClient ("0f2d33639109363ae35474c47cfbd9da", true);
			var questions = client.getFormQuestions (long.Parse(input.Data["FormID"]));

			Dictionary<int, string> questionIdNameMap = new Dictionary<int, string> ();

			var content = questions ["content"].Children();

			foreach (Newtonsoft.Json.Linq.JProperty item in content) {
				var id = int.Parse(item.Name);
				string name = (string)item.Value["name"];
				questionIdNameMap [id] = name;
			}

			var submission = client.getSubmission (long.Parse(input.Data["SubmissionID"]));
			var answers = submission["content"]["answers"].Children();

			var jotFormData = new Dictionary<string,string> ();

			foreach (Newtonsoft.Json.Linq.JProperty answer in answers) {
				var qid = int.Parse(answer.Name);
				var answerValue = answer.Value["answer"];

				// TODO: How to handle nested JSON data?
				if (answerValue is Newtonsoft.Json.Linq.JObject) {
					foreach (Newtonsoft.Json.Linq.JProperty prop in answerValue) {
						var questionName = string.Join (".", new List<string> () { questionIdNameMap [qid], prop.Name });
						jotFormData[questionName] = prop.ToObject<string> ();
					}
				}
				else {
					jotFormData [questionIdNameMap [qid]] = answerValue.ToString ();
				}
			}

			return OutputMessageTemplate.Map(new EventMessage(){ Data = jotFormData});
		}
	}
}

