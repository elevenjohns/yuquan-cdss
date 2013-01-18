using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YuQuan.Models
{
    /// <summary>
    /// Rreasoning Context for rule engine
    /// Item keys in the dictonaries are related to "ContextItemDefinition" in db.
    /// </summary>
    //
    // Implicit and explicit implementation of interface
    //
    //Implicit is when you define your interface via a member on your class. Explicit is when you define methods within your class on the interface. I know that sounds confusing but here is what I mean: IList.CopyTo would be implicitly implememnted as:
    //public void CopyTo(Array array, int index)
    //{
    //    throw new NotImplementedException();
    //}
    //and explicity as:
    //
    //    void ICollection.CopyTo(Array array, int index)
    //    {
    //        throw new NotImplementedException();
    //    }
    //The difference being that implicitly is accessible throuh your class you created when it is cast as that class as well as when its cast as the interface. Explicit implentation allows it to only be accessible when cast as the interface itself.
    //myclass.CopyTo //invalid with explicit
    //((IList)myClass).CopyTo //valid with explicit.
    [DataContract]
    public class Context:ICloneable
    {
        [DataMember]
        public IDictionary<string, string> StringParameters = new Dictionary<string, string>();
        [DataMember]
        public IDictionary<string, double> NumericParameters = new Dictionary<string, double>();
        [DataMember]
        public IDictionary<string, bool> BooleanParameters = new Dictionary<string, bool>();

        void Clear()
        {
            StringParameters.Clear();
            NumericParameters.Clear();
            BooleanParameters.Clear();
        }

        public IEnumerable<string> GetAllKeys()
        {
            return NumericParameters.Keys.Union(
                BooleanParameters.Keys.Union(
                StringParameters.Keys));
        }

        public bool SetValue(Fact fact)
        {
            if (fact == null || fact.ContextItemDefinition == null)
                return false;

            if (fact.ContextItemDefinition.DataType == EnumDataType.数值型.ToString())
            {
                return SetValue(fact.ContextItemDefinition.Name, fact.NumericValue);
            }
            else if (fact.ContextItemDefinition.DataType == EnumDataType.布尔型.ToString())
            {
                return SetValue(fact.ContextItemDefinition.Name, fact.BooleanValue);
            }
            else if (fact.ContextItemDefinition.DataType == EnumDataType.字符串型.ToString())
            {
                return SetValue(fact.ContextItemDefinition.Name, fact.StringValue);
            }
            return false;
        }

        public string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key))
                return "";

            if (NumericParameters.Keys.Contains(key))
                return NumericParameters[key].ToString();
            if (BooleanParameters.Keys.Contains(key))
                return BooleanParameters[key].ToString();
            if (StringParameters.Keys.Contains(key))
                return StringParameters[key];

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>true if successful, false failed</returns>
        public bool SetValue(string key, object value)
        {
            //if(value.GetType() == typeof(double))
            if (value is double && NumericParameters.Keys.Contains(key))
            {                
                NumericParameters[key] = (double)value;
                return true;
            }
            else if (value is bool && BooleanParameters.Keys.Contains(key))
            {
                BooleanParameters[key] = (bool)value;
                return true;
            }
            else if (value is string && StringParameters.Keys.Contains(key))
            {
                StringParameters[key] = (string)value;
                return true;
            }
            return false;
        }

        public object Clone()
        {
            DataContractSerializer serializer = new DataContractSerializer(this.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, this);
            String serializedString = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Position);
            stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedString));
            return serializer.ReadObject(stream);
        }

        
        /// <summary>
        /// Operator override
        /// Merge too context by union operation.
        /// No different values for same key should be guranteed.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Context operator +(Context lhs,Context rhs)
        {
            return Union(lhs, rhs);
        }

        private static Context Union(Context lhs, Context rhs)
        {
            var context = new Context();

            (lhs.NumericParameters.Union(rhs.NumericParameters,new KeyPairComparer<double>())).ToList().ForEach(x => context.NumericParameters.Add(x));
            (lhs.BooleanParameters.Union(rhs.BooleanParameters,new KeyPairComparer<bool>())).ToList().ForEach(x=>context.BooleanParameters.Add(x));
            (lhs.StringParameters.Union(rhs.StringParameters,new KeyPairComparer<string>())).ToList().ForEach(x=>context.StringParameters.Add(x));
            //(lhs.NumericParameters.Keys.Union(rhs.NumericParameters.Keys)).ToList().ForEach(x=>context.NumericParameters.Add(x,0));
            //(lhs.BooleanParameters.Keys.Union(rhs.BooleanParameters.Keys)).ToList().ForEach(x=>context.BooleanParameters.Add(x,false));
            //(lhs.StringParameters.Keys.Union(rhs.StringParameters.Keys)).ToList().ForEach(x=>context.StringParameters.Add(x,""));
            return context;
        }

        /// <summary>
        /// The nested class providing comparison functionality for KeyValuePair
        /// </summary>
        /// <typeparam name="T">The Value Type of pair</typeparam>
        private class KeyPairComparer<T> : IEqualityComparer<KeyValuePair<string, T>>
        {
            #region IEqualityComparer<KeyValuePair<string,T>> Members

            bool IEqualityComparer<KeyValuePair<string, T>>.Equals(KeyValuePair<string, T> x, KeyValuePair<string, T> y)
            {
                return x.Key == y.Key;
            }

            int IEqualityComparer<KeyValuePair<string, T>>.GetHashCode(KeyValuePair<string, T> obj)
            {
                return obj.Key.GetHashCode();
            }

            #endregion
        }
    }
}
