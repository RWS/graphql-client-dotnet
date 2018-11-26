using System.Collections.Generic;

namespace Sdl.Tridion.Api.IqQuery.Model.Field
{
    /// <summary>
    /// Grouped Field
    /// </summary>
    public class GroupedField : BaseField
    {
        public List<string> Names { get; set; }

        public List<object> Values { get; set; } = new List<object>();

        public List<ITermValue> TermValues { get; set; } = new List<ITermValue>();

        public List<EntityFieldType> FieldTypes { get; set; }

        public bool IsTerm => TermValues.Count > 0;
     
        public GroupedField(bool negate) : base(negate)
        {
        }

        public GroupedField(bool negate, List<string> names, List<object> values) : base(negate)
        {
            if (values.Count != 1 && values.Count != names.Count)
            {
                throw new FieldException(
                    "Invalid usage of multi value search. Size of values should be one or equal to size of names.");
            }

            Names = names;
            FieldTypes = new List<EntityFieldType>();
            foreach (var x in values)
            {
                if (x is ITermValue)
                {                   
                    ITermValue termValue = (ITermValue) x;
                    TermValues.Add(termValue);
                }
                else
                {
                    Values.Add(x);
                }
                FieldTypes.Add(FieldUtils.DetectFieldType(x));
            }

            if(TermValues.Count>0 && Values.Count>0)
                throw new FieldException("Invalid usage of multi value search. All values should be either term values or not.");
        }
    }
}
