using System;
using System.Collections.Generic;
using Unity.Netcode;

namespace Extensions.NetworkExtensions
{
    [System.Serializable]
    public class NetworkDictionary<TKey, TValue> : INetworkSerializable, IEquatable<NetworkDictionary<TKey, TValue>>
    {
        public Dictionary<TKey, TValue> MyDictionary = new Dictionary<TKey, TValue>();

        public TValue this[TKey p_key]
        {
            get
            {
                if (MyDictionary.TryGetValue(p_key, out TValue l_value))
                {
                    return l_value;
                }
                throw new KeyNotFoundException($"Key '{p_key}' not found in the dictionary.");
            }
            set
            {
                MyDictionary[p_key] = value;
            }
        }
        
        
        public void NetworkSerialize<T>(BufferSerializer<T> p_serializer) where T : IReaderWriter
        {
            // Serializamos la longitud del diccionario
            int l_count = MyDictionary.Count;
            p_serializer.SerializeValue(ref l_count);

            if (p_serializer.IsReader)
            {
                MyDictionary.Clear();
                for (int l_i = 0; l_i < l_count; l_i++)
                {
                    TKey l_key = default;
                    TValue l_value = default;

                    // Serialización de la clave
                    SerializePrimitive(p_serializer, ref l_key);

                    // Serialización del valor
                    SerializePrimitive(p_serializer, ref l_value);

                    MyDictionary[l_key] = l_value;
                }
            }
            else
            {
                foreach (var l_kvp in MyDictionary)
                {
                    TKey l_key = l_kvp.Key;
                    TValue l_value = l_kvp.Value;

                    // Serialización de la clave
                    SerializePrimitive(p_serializer, ref l_key);

                    // Serialización del valor
                    SerializePrimitive(p_serializer, ref l_value);
                }
            }
        }

        private void SerializePrimitive<T, TU>(BufferSerializer<T> p_serializer, ref TU p_data) where T : IReaderWriter
        {
            if (typeof(TU) == typeof(string))
            {
                // Manejar string específicamente
                string l_temp = (string)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else if (typeof(TU) == typeof(int))
            {
                int l_temp = (int)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else if (typeof(TU) == typeof(float))
            {
                float l_temp = (float)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else if (typeof(TU) == typeof(byte))
            {
                byte l_temp = (byte)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else if (typeof(TU) == typeof(bool))
            {
                bool l_temp = (bool)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else if (typeof(TU) == typeof(long))
            {
                long l_temp = (long)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else if (typeof(TU) == typeof(double))
            {
                double l_temp = (double)(object)p_data;
                p_serializer.SerializeValue(ref l_temp);
                p_data = (TU)(object)l_temp;
            }
            else
            {
                throw new System.NotSupportedException($"Unsupported type {typeof(TU)} for serialization in NetworkDictionary.");
            }
        }

        // Métodos adicionales para manipular el diccionario
        public void Add(TKey p_key, TValue p_value)
        {
            MyDictionary[p_key] = p_value;
        }

        public bool TryGetValue(TKey p_key, out TValue p_value)
        {
            return MyDictionary.TryGetValue(p_key, out p_value);
        }

        public void Remove(TKey p_key)
        {
            MyDictionary.Remove(p_key);
        }

        public bool ContainsKey(TKey p_key) => MyDictionary.ContainsKey(p_key);
        
        
        public Dictionary<TKey, TValue>.KeyCollection Keys => MyDictionary.Keys;
        public Dictionary<TKey, TValue>.ValueCollection Values => MyDictionary.Values;
        
        public bool Equals(TKey other)
        {
            return false;
        }

        public bool Equals(NetworkDictionary<TKey, TValue> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(MyDictionary, other.MyDictionary);
            
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NetworkDictionary<TKey, TValue>)obj);
        }

        public override int GetHashCode()
        {
            return (MyDictionary != null ? MyDictionary.GetHashCode() : 0);
        }
    }
}