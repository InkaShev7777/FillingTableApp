
using FillingTable.Utilities;

namespace FillingTable.Model.Fields
{
    internal class FloatField : IField
    {
        public Type Type { get; private set; }
        public string Name { get; set; }
        public string Value { get; private set; }
        public bool IsUnique { get; private set; }

        public bool IsForeign { get; private set; }
        public FloatField(string collumData)
        {
            Type = FieldType.SelectType(collumData);
            Name = Parser.GetName(collumData, false);
            IsUnique = Parser.IsUnique(collumData);
            IsForeign = Parser.IsForeign(collumData);
        }

        public void SetValue(string value) => Value = value;
        public string GetFieldName() => Name;
        public Type GetFieldType() => Type;
        bool IField.IsForeign() => IsForeign;

    }
}
