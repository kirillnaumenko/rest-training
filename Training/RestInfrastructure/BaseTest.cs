using System;
//using NLog;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RestSharp;
using Training.RestInfrastructure;

namespace Training.Tasks
{
	public abstract class BaseTest : InfrastructureObject
	{
		protected ILogger logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger(string.Empty);
		protected Actions actions = new Actions();
		//protected Logger logger = LogManager.GetLogger("logfile");
    }
}

