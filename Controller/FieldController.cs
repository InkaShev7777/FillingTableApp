using Bogus;
using FillingTable.Model.Fields;
using FillingTable.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Type = FillingTable.Utilities.Type;

namespace FillingTable.Controller
{
    internal class FieldController
    {
        public event Action<string> ShowingData;
        private List<string> _query;
        private List<Field> _fields;
        private List<Field> _allFields;
        private Faker _faker;
        private int _querryCount;
        public FieldController()
        {
            _querryCount = 10;
            _faker = new Faker();
            _query = new List<string>();
            _fields = new List<Field>();
            _allFields = new List<Field>();
        }
        public void SetQuertCount(int count) 
        {
            if (count > 0)
                _querryCount = count;
        }
        public void SetSqlData(string data)
        {
            _query.Clear();
            _allFields.Clear();
            _fields.Clear();
            data = data.Trim();
            string[] sql = data.Split('\n');
            CreateFields(sql);
            CreateQuery(sql);
        }

        private void CreateQuery(string[] data)
        {
            string tableName = Parser.GetName(data[0], true);
            _query.Add($"INSERT INTO {tableName}\n");
            _query.Add(GetFieldsNames());
            SetDataToFields();
            _query.Add("VALUES\n");
            AddValuesIntoQuery();
            ShowingData?.Invoke(GetQuery());
        }

        private string GetQuery()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _query)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        private void CreateFields(string[] data)
        {
            foreach (var item in data)
            {
                if (!item.ToLower().Contains("not null"))
                    continue;
                if (item.ToLower().Contains("identity"))
                    continue;

                int maxLenght = 0;
                if (item.IndexOf('(') > 0)
                    maxLenght = Convert.ToInt32(item.Substring(item.IndexOf('(') + 1, item.IndexOf(')') - item.IndexOf('(') - 1));

                Type type = FieldType.SelectType(item);
                _fields.Add(FieldFactory.CreateField(type, item, maxLenght));
            }

            for (int i = 0; i < _querryCount; i++)
            {
                for (int j = 0; j < _fields.Count; j++)
                {
                    if (_fields[j].Type.Equals(Type.INT))
                        _allFields.Add(new IntField(_fields[j]));
                    if (_fields[j].Type.Equals(Type.FLOAT))
                        _allFields.Add(new FloatField(_fields[j]));
                    if (_fields[j].Type.Equals(Type.STRING))
                        _allFields.Add(new StringField(_fields[j], (_fields[j] as StringField).MaxLenght));
                    if (_fields[j].Type.Equals(Type.DATE))
                        _allFields.Add(new DateField(_fields[j]));
                }
            }
            
        }

        private string GetFieldsNames()
        {
            string fieldsForInsert = "(";
            foreach (var item in _fields)
            {
                string name =  item.Name+",";
                fieldsForInsert += name;
            }
            fieldsForInsert = fieldsForInsert.Remove(fieldsForInsert.Length - 1, 1);
            fieldsForInsert += ")";
            return fieldsForInsert+"\n";
        }

        private void SetDataToFields()
        {
            for (int k = 0; k < _allFields.Count; k++)
            {
                if (_allFields[k].Name.ToLower().Equals("[lastname]"))
                    _allFields[k].SetValue($"'{_faker.Name.LastName()}'");
                else if (_allFields[k].Name.ToLower().Equals("[firstname]"))
                    _allFields[k].SetValue($"'{_faker.Name.LastName()}'");
                else if (_allFields[k].Name.ToLower().Equals("[fullname]"))
                    _allFields[k].SetValue($"'{_faker.Name.FullName()}'");
                else if (_allFields[k].Name.ToLower().Equals("[birthday]"))
                    _allFields[k].SetValue($"'{_faker.Date.Past(80).ToString("yyyy-MM-dd")}'");
                else if (_allFields[k].Name.ToLower().Equals("[city]"))
                    _allFields[k].SetValue($"'{_faker.Address.City()}'");
                else if (_allFields[k].Name.ToLower().Equals("[country]"))
                    _allFields[k].SetValue($"'{_faker.Address.Country()}'");
                else if (_allFields[k].Name.ToLower().Equals("[street]"))
                    _allFields[k].SetValue($"'{_faker.Address.StreetAddress()}'");
                else if (_allFields[k].Name.ToLower().Equals("[phonenumber]"))
                    _allFields[k].SetValue($"'{_faker.Phone.PhoneNumber()}'");
                else if (_allFields[k].Name.ToLower().Equals("[age]"))
                    _allFields[k].SetValue($"'{_faker.Random.Number(100)}'");
                else
                {
                    if (_allFields[k].Type.Equals(Type.INT))
                        _allFields[k].SetValue($"'{_faker.Random.Number(999999).ToString()}'");
                    else if (_allFields[k].Type.Equals(Type.FLOAT))
                        _allFields[k].SetValue($"'{_faker.Random.Float(0.1f, 9999.0f).ToString(new CultureInfo("en-US"))}'");
                    else if (_allFields[k].Type.Equals(Type.STRING))
                        _allFields[k].SetValue($"'{_faker.Lorem.Sentence()}'");
                    else if (_allFields[k].Type.Equals(Type.DATE))
                        _allFields[k].SetValue($"'{_faker.Date.Past(50).ToString("yyyy-MM-dd")}'");
                }

                if (_allFields[k].IsForeign)
                    _allFields[k].SetValue("'REPLACE THIS ON YOUR KEY FROM ANOTHER TABLE'");
            }
        }

        private void AddValuesIntoQuery()
        {
            string res = "(";
            for (int k = 0; k < _allFields.Count-1; k++)
            {
                if (k != 0 && k % _fields.Count == 0)
                {
                    res = res.Remove(res.Length - 1, 1);
                    res += "),\n";
                    _query.Add(res);
                    res = "(";
                }
                res += $"{_allFields[k].Value},"; 
            }
            string last = _query.Last();
            int ind = last.LastIndexOf(',');
            last = last.Remove(ind, 1);
            last = last.Insert(ind, ";");
            _query[_query.Count-1] = last;
        }
    }
}
