using System;
using System.Collections.Generic;
using Microsoft.Framework.Logging;
using ILogger = Microsoft.Framework.Logging.ILogger;
using Microsoft.AspNet.Mvc;
using ScaleBridge.Core;
using ScaleBridge.Message;
using ScaleBridge.Message.Event;
using NServiceBus;


namespace ScaleBridge.Web.Controllers
{
    //[RequireAuthorization]
    public class MessageController : Controller
    {
        public IBus Bus { get; set; }
        public ILogger Logger { get; set; }
         
        //@bweller
        //This is an example of injecting the below dependencies to the Controllers
        //ASPNET 5 has dependency injection builtin
        public MessageController(ILoggerFactory loggerFactory, IBus bus)
        {
            Logger = loggerFactory.CreateLogger(typeof(MessageController).FullName);
            Bus = bus;
        }
        
    	[Route("api/message")]
        public void Entry([FromBody]Dictionary<string,string>message)
        {
            try{
                Logger.LogInformation("Message received");
                
                Bus.Send("ScaleBridge.Transform", new InputMessage(){
                    MessageData = message
                });
            }
            catch(Exception ex){
                Logger.LogError(ex.Message);
                Logger.LogError(ex.StackTrace);
                throw ex;
            }
        }
       
    }
}