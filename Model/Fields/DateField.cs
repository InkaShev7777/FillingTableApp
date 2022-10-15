using FillingTable.Utilities;
using System;
using Type = FillingTable.Utilities.Type;

namespace FillingTable.Model.Fields
{
    internal class DateField : Field
    {
        public DateField(string collumData) : base(collumData)
        {
        }

        public DateField(Field field) : base(field)
        {
        }

        public override void SetValue(string value) => Value = value;


    }
}
