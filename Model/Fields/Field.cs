using FillingTable.Utilities;
using Type = FillingTable.Utilities.Type;

namespace FillingTable.Model.Fields
{
    public abstract class Field
    {
        public Type Type { get; protected set; }
        public string Name { get; set; }
        public string Value { get; protected set; }
        public bool IsUnique { get; protected set; }

        public bool IsForeign { get; protected set; }

        public  Field(string collumData)
        {
            Type = FieldType.SelectType(collumData);
            Name = Parser.GetName(collumData, false);
            IsUnique = Parser.IsUnique(collumData);
            IsForeign = Parser.IsForeign(collumData);
        }
        public Field(Field field)
        {
            Type = field.Type;
            Name = field.Name;
            Value = field.Value;
            IsUnique = field.IsUnique;
            IsForeign = field.IsForeign;
        }

        public abstract void SetValue(string value);


    }
}
