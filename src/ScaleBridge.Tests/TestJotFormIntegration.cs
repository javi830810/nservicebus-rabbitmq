using NUnit.Framework;
using System;
using ScaleBridge.Core;
using ScaleBridge.Message.Object;
using ScaleBridge.Message.Event;
using System.Collections.Generic;
using JF = ScaleBridge.Partner.JotForm;

namespace ScaleBridge.Tests
{
	[TestFixture ()]
	public class TestJotFormIntegration
	{
		ITransformAction action;

		[SetUp]
		public void Setup()
		{
			action = new JF.JotFormAPIAction () {
				OutputMessageTemplate = new MessageTemplate () {
					MessageType = "BitrixMessage",
					PropertyMaps = new Dictionary<string, string> () {
						{ "fullName" , "fullName" },
						{ "emailAddress" , "emailAddress" },

					}
				}
			};
		}

		[Test ()]
		public void TestTransformAction ()
		{
			var inputMessage = new EventMessage () {
				MessageType = "JotFormBaseMessage",
				Data = new Dictionary<string, string> () {
					{ "FormID" , "51265455222147" },
					{ "SubmissionID" , "306831371753633797" }
				}
			};

			var outputMessage = action.Transform (inputMessage);

			Assert.AreEqual (outputMessage.Data["emailAddress"], "bweller@kaplan.edu");
		}
	}
}

