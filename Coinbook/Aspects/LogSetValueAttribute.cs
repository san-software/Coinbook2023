using PostSharp.Aspects;
using PostSharp.Serialization;
using SAN.Logging;
using System.Collections.Generic;
using System.Text;

namespace Postsharp.LogMethodAttribute
{
    /// <summary>
    ///   Aspect that, when applied to a field or property, appends a record to the <see cref="Logger" /> class whenever this
    ///   field or property is set to a new value.
    /// </summary>
    [PSerializable]
    public sealed class LogSetValueAttribute : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            List<ParameterModel> parameter = new List<ParameterModel>();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Setting ");
            Formatter.AppendTypeName(stringBuilder, args.Location.DeclaringType);
            if (args.Index.Count != 0)
            {
                Formatter.AppendArguments(stringBuilder, args.Index);
            }

            stringBuilder.Append(" = ");
            stringBuilder.Append(args.Value);

            Logger.WriteLine(stringBuilder.ToString(), LogLevels.Info, parameter);

            base.OnSetValue(args);
        }
    }
}
