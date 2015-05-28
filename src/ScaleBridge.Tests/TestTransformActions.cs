using NUnit.Framework;
using System;
using ScaleBridge.Core;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;
using System.Collections.Generic;

namespace ScaleBridge.Tests
{
	[TestFixture ()]
	public class TestTransformationActions
	{
		ITransformAction action;

		[SetUp]
		public void Setup()
		{

		}

		[Test ()]
		public void TestTransformAction ()
		{
			action = new MapTransformAction () {
				OutputMessageTemplate = new MessageTemplate(){
					MessageType = "Y",
					PropertyMaps = new Dictionary<string, string>(){
						{"x1" , "y1"},
						{"x2" , "y2"},
						{"x3" , "y3"}
					}
				}
			};

			var inputMessage = new EventMessage () {
				MessageType = "X",
				Data = new Dictionary<string, string> () {
					{ "x1" , "0" },
					{ "x2" , "1" },
					{ "x3" , "2" }
				}
			};

			var outputMessage = action.Transform (inputMessage);

			Assert.IsTrue (outputMessage.Data.ContainsKey ("y1"));
			Assert.IsTrue (outputMessage.Data.ContainsKey ("y2"));
			Assert.IsTrue (outputMessage.Data.ContainsKey ("y3"));

			Assert.AreEqual (outputMessage.Data["y1"], "0");
			Assert.AreEqual (outputMessage.Data["y2"], "1");
			Assert.AreEqual (outputMessage.Data["y3"], "2");
		}

		[Test ()]
		public void TestAPITransformAction ()
		{
			//This action have to consume an API and generate a transformed Message
			//It will use the following testing API for reference
			//http://jsonplaceholder.typicode.com/posts/1

			action = new APITransformAction () {
				Method = "GET",
				Url = "http://jsonplaceholder.typicode.com/posts/1",

				OutputMessageTemplate = new MessageTemplate(){
					MessageType = "Y",
					PropertyMaps = new Dictionary<string, string>(){
						{"userId" , "y1"},
						{"id" , "y2"},
						{"body" , "y3"}
					}
				}
			};

			var inputMessage = new EventMessage () {
				MessageType = "X",
				Data = new Dictionary<string, string> () {
					{ "x1" , "0" },
					{ "x2" , "1" },
					{ "x3" , "2" }
				}
			};

			var outputMessage = action.Transform (inputMessage);

			Assert.IsTrue (outputMessage.Data.ContainsKey ("y1"));
			Assert.IsTrue (outputMessage.Data.ContainsKey ("y2"));
			Assert.IsTrue (outputMessage.Data.ContainsKey ("y3"));

			Assert.AreEqual (outputMessage.Data["y1"], "1");
			Assert.AreEqual (outputMessage.Data["y2"], "1");
			Assert.AreEqual (outputMessage.Data["y3"].Length, "quia et suscipit suscipit recusandae consequuntur expedita et cum reprehenderit molestiae ut ut quas totam nostrum rerum est autem sunt rem eveniet architecto".Length);

		}
	}
}

