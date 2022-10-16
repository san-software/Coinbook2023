using PostSharp.Aspects;
using PostSharp.Serialization;
using SAN.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Postsharp.LogMethodAttribute
{
    /// <summary>
    ///   Aspect that, when applied to a method, appends a record to the <see cref="Logger" /> class whenever this method is
    ///   executed.
    /// </summary>
    [PSerializable]
    [LinesOfCodeAvoided(6)]
    public sealed class LogMethodAttribute : OnMethodBoundaryAspect
    {
        /// <summary>
        ///   Method invoked before the target method is executed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!LogHelper.LogSettings.On)
                return;

            List<ParameterModel> parameter = new List<ParameterModel>();
            var stringBuilder = new StringBuilder();

            MethodExecutionArgs x = args;

            parameter.Add(new ParameterModel() { Name = args.Method.Name, Value = string.Empty });

            if (args.Method.Name.Length >=4 && args.Method.Name.Substring(0,4) =="set_")
                return; 
            else if (args.Method.Name.Length >= 4 && args.Method.Name.Substring(0, 4) == "get_")
                return; 
            else if (args.Arguments.Count > 0)
            {
                foreach (var p in args.Arguments)
                {
                    parameter.Add(new ParameterModel() { Name = args.Method.Name, Value = p });
                }
            }

            stringBuilder.Append("Entering ");
            AppendCallInformation(args, stringBuilder);

            if (LogHelper.LogSettings.All)
            {
                if (LogHelper.LogSettings.Level == LogLevels.Info)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Info, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Warn)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Warn, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Debug)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Debug, parameter);
            }
            else
            {
                if (LogHelper.LogSettings.Level == LogLevels.Info)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Info, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Warn)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Warn, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Debug)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Debug, parameter);
            }

            Logger.Indent();

        }

        /// <summary>
        ///   Method invoked after the target method has successfully completed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (!LogHelper.LogSettings.On)
                return;

            Logger.Unindent();

            List<ParameterModel> parameter = new List<ParameterModel>();
            var stringBuilder = new StringBuilder();

            MethodExecutionArgs x = args;

            if (args.Method.Name.Length >= 4 && args.Method.Name.Substring(0, 4) == "set_")
                return; 
            else if (args.Method.Name.Length >= 4 && args.Method.Name.Substring(0, 4) == "get_")
                return; 
            else if (args.Arguments.Count > 0)
            {
                foreach (var p in args.Arguments)
                {
                    parameter.Add(new ParameterModel() { Name = args.Method.Name, Value = p });
                }
            }

            stringBuilder.Append("Exiting ");
            AppendCallInformation(args, stringBuilder);

            if (args.Method.ToString() != "Void .cctor()")
            {
                if (!args.Method.IsConstructor && ((MethodInfo)args.Method).ReturnType != typeof(void))
                {
                    stringBuilder.Append(" with return value ");
                    stringBuilder.Append(args.ReturnValue);
                }
            }

            if (LogHelper.LogSettings.All)
            {
                if (LogHelper.LogSettings.Level == LogLevels.Info)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Info, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Warn)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Warn, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Debug)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Debug, parameter);
            }
            else
            {
                if (LogHelper.LogSettings.Level == LogLevels.Info)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Info, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Warn)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Warn, parameter);

                if (LogHelper.LogSettings.Level == LogLevels.Debug)
                    Logger.WriteLine(stringBuilder.ToString(), LogLevels.Debug, parameter);
            }
        }

        /// <summary>
        ///   Method invoked when the target method has failed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            MethodExecutionArgs x = args;

            if (args.Arguments.Count > 0)
                x = args;

            Logger.Unindent();
            List<ParameterModel> parameter = new List<ParameterModel>();
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Exiting ");
            AppendCallInformation(args, stringBuilder);

            if (!args.Method.IsConstructor && ((MethodInfo)args.Method).ReturnType != typeof(void))
            {
                stringBuilder.Append(" with exception ");
                stringBuilder.Append(args.Exception.GetType().Name);
            }

            Logger.WriteLine(stringBuilder.ToString(), LogLevels.Error, parameter);

            string title = "Fehler in " + args.Method.Name;
            string text = args.Exception.Message + Environment.NewLine + Environment.NewLine + args.Exception.StackTrace;
            MessageBox.Show(text, title, MessageBoxButton.OK, MessageBoxImage.Error);

            System.Windows.Forms.Application.Exit();
        }

        private static void AppendCallInformation(MethodExecutionArgs args, StringBuilder stringBuilder)
        {
            var declaringType = args.Method.DeclaringType;
            Formatter.AppendTypeName(stringBuilder, declaringType);
            stringBuilder.Append('.');
            stringBuilder.Append(args.Method.Name);

            if (args.Method.IsGenericMethod)
            {
                var genericArguments = args.Method.GetGenericArguments();
                Formatter.AppendGenericArguments(stringBuilder, genericArguments);
            }

            var arguments = args.Arguments;

            Formatter.AppendArguments(stringBuilder, arguments);
        }
    }
}
