
namespace FillingTable.Model.Fields
{
    internal class IntField : Field
    {
        public IntField(string collumData) : base(collumData)
        {
        }

        public IntField(Field field) : base(field)
        {
        }

        public override void SetValue(string value) => Value = value;

    }
}
