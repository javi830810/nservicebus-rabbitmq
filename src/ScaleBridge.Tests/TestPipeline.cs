using NUnit.Framework;
using System;
using ScaleBridge.Core;
using ScaleBridge.Message.Object;
using ScaleBridge.Message;
using System.Collections.Generic;

namespace ScaleBridge.Tests
{
	[TestFixture ()]
	public class TestPipeline
	{
		ActionTreePipeline pipeline;

		[SetUp]
		public void Setup()
		{
			this.pipeline = new ActionTreePipeline () {
				Current = new APITransformAction () {
					Method = "GET",
					Url = "http://jsonplaceholder.typicode.com/posts/1",

					OutputMessageTemplate = new MessageTemplate () {
						MessageType = "Y",
						PropertyMaps = new Dictionary<string, string> () {
							{ "userId" , "y1" },
							{ "id" , "y2" },
							{ "body" , "y3" }
						}
					}
				},
				Children = new List<ActionTreePipeline> () {
					new ActionTreePipeline () {
						Current = new MapTransformAction () {
							OutputMessageTemplate = new MessageTemplate () {
								MessageType = "Y",
								PropertyMaps = new Dictionary<string, string> () {
									{ "y1" , "z1" },
									{ "y2" , "z2" },
								}
							}
						}
					}
				}
			};
		}

		[Test ()]
		public void TestTransformAction ()
		{
			var inputMessage = new EventMessage () {
				MessageType = "X",
				Data = new Dictionary<string, string> () {
					{ "x1" , "0" },
					{ "x2" , "1" },
					{ "x3" , "2" }
				}
			};

			pipeline.Execute(inputMessage);
		}
	}
}

