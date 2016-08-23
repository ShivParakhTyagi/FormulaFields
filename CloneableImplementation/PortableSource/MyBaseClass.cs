using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace PortableSource
{
    public class Data
    {

        public string A { get; set; }
        public string TYPE { get; set; }
        public int B { get; set; }
        
        public override string ToString()
        {
            return $"{A}+{B}";
        }

    }

    public class MyBaseClass
    {
        //public MyBaseClass()
        //{
            
        //}
        //protected MyBaseClass(MyBaseClass baseClass)
        //{
        //    FormName = baseClass.FormName;
        //    Data = baseClass.Data;
        //    FormDataName = baseClass.DataClass?.Clone() as Data;
        //    DataClass = baseClass.DataClass;
        //    ParentDataList = new List<string>(baseClass.ParentDataList);
        //    DataList=new List<string>(baseClass.DataList);
        //    ParentDataClassList = new List<Data>(baseClass.ParentDataClassList);
        //    DataClassList = new List<Data>(baseClass.DataClassList);
            
        //}

        [JsonIgnore]
        public string FormName { get; set; }

        public string Data { get; set; }

        [JsonIgnore]
        public Data FormDataName { get; set; }

        public Data DataClass { get; set; }

        [JsonIgnore]
        public List<string> ParentDataList { get; set; }

        public List<string> DataList { get; set; }
        [JsonIgnore]
        public List<Data> ParentDataClassList { get; set; }

        public List<Data> DataClassList { get; set; }

        //public object Clone()
        //{
        //    return new MyBaseClass(this);
        //}

        [JsonIgnore]
        public List<Data> FieldKeys { get; set; }

    }

    public class MySpecialClass: MyBaseClass, IDictionary<string, object>, INotifyPropertyChanged
    {
        private Dictionary<string, object> _fieldValues;

        public Dictionary<string, object> FieldValues
        {
            get
            {
                if (_fieldValues == null)
                {
                    _fieldValues = new Dictionary<string, object>();
                }
                return _fieldValues;
            }
            set { _fieldValues = value; }
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.FieldValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.FieldValues.GetEnumerator();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.FieldValues.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.FieldValues.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            object itemVal = null;
            if (this.FieldValues.TryGetValue(item.Key, out itemVal))
            {
                if (itemVal == item.Value)
                {
                    this.FieldValues.Remove(item.Key);
                    return true;
                }
            }

            return false;
        }

        public int Count
        {
            get { return this.FieldValues.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(string key, object value)
        {
            if (this.FieldValues.ContainsKey(key))
            {
                this.FieldValues[key] = value;
            }
            else
            {
                this.FieldValues.Add(key, value);
            }

            OnPropertyChanged(key);
        }


        public bool ContainsKey(string key)
        {
            return this.FieldValues.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return this.FieldValues.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return this.FieldValues.TryGetValue(key, out value);
        }

        public new object this[string key]
        {
            get
            {
                if (this.FieldValues.ContainsKey(key))
                {
                    return this.FieldValues[key];
                }
                else
                {
                    return null;
                }
            }
            set { this.Add(key, value); }
        }

        public ICollection<string> Keys
        {
            get { return this.FieldValues.Keys; }
        }

        public ICollection<object> Values
        {
            get { return this.FieldValues.Values; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string key)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
        }

        internal MySpecialClass Copy()
        {
            MySpecialClass newInstance = new MySpecialClass();
            return newInstance;
        }



        //public MySpecialClass ShallowCopy()
        //{
        //    return (MySpecialClass)this.MemberwiseClone();
        //}

        //public MySpecialClass DeepCopy()
        //{
        //    MySpecialClass other = (MySpecialClass)this.MemberwiseClone();
        //    return other;
        //}
    }
}
