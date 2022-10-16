using System;
using System.Collections.Generic;
using System.Text;
using PostSharp.Aspects;

namespace SAN.Exception
{
	[Serializable]
	public sealed class QuickTraceAttribute : OnMethodBoundaryAspect
	{
		private string enteringMessage;
		private string leavingMessage;

		public override void CompileTimeInitialize(System.Reflection.MethodBase method, AspectInfo aspectInfo)
		{
			string methodName = method.DeclaringType.FullName + "." + method.Name;
			enteringMessage = "Entering " + methodName;
			leavingMessage = "Leaving  " + methodName;
		}

		public override void OnEntry(MethodExecutionArgs args)
		{
			Logger1.Log(Logger1.LogLevel.Info, enteringMessage);
		}

		public override void OnExit(MethodExecutionArgs args)
		{
			Logger1.Log(Logger1.LogLevel.Info, leavingMessage);
		}

		public override void OnException(MethodExecutionArgs args)
		{
			Logger1.Log(Logger1.LogLevel.Error, leavingMessage + " with exception: " + args.Exception.Message + Environment.NewLine + args.Exception.ToString());
		}
	}

	[Serializable]
	public sealed class FullTraceAttribute : OnMethodBoundaryAspect
	{
		private MethodFormatStrings methodFormatStrings;

		public override void CompileTimeInitialize(System.Reflection.MethodBase method, AspectInfo aspectInfo)
		{
			this.methodFormatStrings = Formatter.GetMethodFormatStrings(method);
		}

		public override void OnEntry(MethodExecutionArgs args)
		{
			Logger1.Log(Logger1.LogLevel.Info,  "Entering " + methodFormatStrings.Format(args.Instance, args.Method, args.Arguments.ToArray()));
			for (int i = 0; i < args.Arguments.Count; i++)
			{
				if (args.Arguments[i] != null)
					Console.WriteLine(args.Arguments[i].ToString());
				else
					Console.WriteLine("Null");
			}

		}

		public override void OnExit(MethodExecutionArgs args)
		{
			Logger1.Log(Logger1.LogLevel.Info,  "Leaving  " + this.methodFormatStrings.Format(args.Instance, args.Method, args.Arguments.ToArray()));
		}

		public override void OnException(MethodExecutionArgs args)
		{
			Logger1.Log(Logger1.LogLevel.Error, "Leaving  " + methodFormatStrings.Format(args.Instance, args.Method, args.Arguments.ToArray()) + " with exception: "
					+ args.Exception.Message + Environment.NewLine + args.Exception.ToString());
		}
	}
}
