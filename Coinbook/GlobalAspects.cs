using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;
using PostSharp.Serialization;
using PostSharp.Aspects;
using System.Diagnostics;

[assembly: Log(AttributePriority = 1, AttributeTargetMemberAttributes = MulticastAttributes.Protected | MulticastAttributes.Internal | MulticastAttributes.Public)]
[assembly: Log(AttributePriority = 2, AttributeExclude = true, AttributeTargetMembers = "get_*")]
[assembly: Trace(AttributeTargetTypes = "Samples4.My*", AttributePriority = 1, Category = "A")]
[assembly: Trace(AttributeTargetTypes = "Samples4.My*", AttributeTargetMemberAttributes = MulticastAttributes.Public, AttributePriority = 2, Category = "B")]


class GlobalAspects
    {
}

//[assembly: Trace(AttributeTargetTypes = "Samples4.My*", AttributePriority = 1, Category = "A")]
//[assembly: Trace(AttributeTargetTypes = "Samples4.My*",
//    AttributeTargetMemberAttributes = MulticastAttributes.Public, AttributePriority = 2, Category = "B")]

[MulticastAttributeUsage(MulticastTargets.Method, AllowMultiple = false)]
[PSerializable]
public sealed class TraceAttribute : OnMethodBoundaryAspect
{
    public string Category { get; set; }

    public override void OnEntry(MethodExecutionArgs args)
    {
        Trace.WriteLine("Entering " + args.Method.DeclaringType.FullName + "." + args.Method.Name, this.Category);
    }
}



