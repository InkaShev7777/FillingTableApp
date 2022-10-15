using FillingTable.Utilities;

namespace FillingTable.Model.Fields
{
    internal class StringField : Field
    {
        public int MaxLenght { get; private set; }
        
        public StringField(string collumData,int maxLenght) : base(collumData)
        {
            MaxLenght = maxLenght;
        }

        public StringField(Field field, int maxLenght) : base(field)
        {
            MaxLenght=maxLenght;
        }

        public override void SetValue(string value)
        {
            if(value.Length > MaxLenght)
            {
                Value = value.Substring(0, MaxLenght-5);
                Value += "'";
                return;
            }
            Value = value;
        }

    }
}
