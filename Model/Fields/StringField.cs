using FillingTable.Utilities;

namespace FillingTable.Model.Fields
{
    internal class StringField : IField
    {
        public int MaxLenght { get; private set; }
        public Type Type { get; private set; }
        public string Name { get; set; }
        public string Value { get; private set; }
        public bool IsUnique { get; private set; }

        public bool IsForeign { get; private set; }
        public StringField(string collumData, int maxLenght)
        {
            Type = FieldType.SelectType(collumData);
            Name = Parser.GetName(collumData, false);
            IsUnique = Parser.IsUnique(collumData);
            IsForeign = Parser.IsForeign(collumData);
            MaxLenght = maxLenght;
        }

        public void SetValue(string value)
        {
            if(value.Length > MaxLenght)
            {
                Value = value.Substring(0, MaxLenght);
                return;
            }
            Value = value;
        }
        public string GetFieldName() => Name;
        public Type GetFieldType() => Type;
        bool IField.IsForeign() => IsForeign;
    }
}
