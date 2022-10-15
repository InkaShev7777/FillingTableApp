
using FillingTable.Utilities;

namespace FillingTable.Model.Fields
{
    internal class FloatField : Field
    {
        public FloatField(string collumData) : base(collumData)
        {
        }

        public FloatField(Field field) : base(field)
        {
        }

        public override void SetValue(string value) => Value = value;


    }
}
