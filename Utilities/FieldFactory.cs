using FillingTable.Model.Fields;
using System;

namespace FillingTable.Utilities
{
    public static class FieldFactory
    {

        public static IField CreateField(Type type, string collumData, int maxLenght)
        {
            switch (type)
            {
                case Type.STRING:
                    return new StringField(collumData, maxLenght);
                case Type.INT:
                    return new IntField(collumData);
                case Type.FLOAT:
                    return new FloatField(collumData);
                case Type.DATE:
                    return new DateField(collumData);
                default:
                    throw new InvalidOperationException("Incorrect type for field");
            }
        }
    }
}
