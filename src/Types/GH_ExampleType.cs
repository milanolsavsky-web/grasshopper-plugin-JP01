// Example custom Grasshopper type. Types hold the data that flows through
// wires between components. Every custom type must implement IGH_Goo.
// This example wraps a simple string value.
//
// Copy this file to create your own type. You will need to:
// 1. Rename the class.
// 2. Replace the _value field and Value property with your data.
// 3. Update CastFrom to handle conversions from other types.
// 4. Update Read/Write if your data has more fields.
// 5. Create a matching parameter (see GH_ExampleParameter).

using System;
using GH_IO.Serialization;
using Grasshopper.Kernel.Types;

namespace PluginName.Types
{
    public class GH_ExampleType : IGH_Goo
    {
        private string _value;

        public GH_ExampleType()
        {
            _value = string.Empty;
        }

        public GH_ExampleType(string value)
        {
            _value = value ?? string.Empty;
        }

        public GH_ExampleType(GH_ExampleType other)
        {
            _value = other._value;
        }

        public string Value => _value;

        public bool IsValid => !string.IsNullOrEmpty(_value);

        public string IsValidWhyNot
        {
            get
            {
                if (string.IsNullOrEmpty(_value))
                {
                    return "Value is empty.";
                }
                return string.Empty;
            }
        }

        public string TypeName => "Example Type";

        public string TypeDescription => "An example custom Grasshopper type.";

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            if (obj is GH_ExampleType other)
            {
                return string.Equals(_value, other._value, StringComparison.Ordinal);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        public IGH_Goo Duplicate()
        {
            return new GH_ExampleType(this);
        }

        public IGH_GooProxy EmitProxy()
        {
            return null;
        }

        public bool CastFrom(object source)
        {
            if (source is string text)
            {
                _value = text;
                return true;
            }
            if (source is GH_ExampleType other)
            {
                _value = other._value;
                return true;
            }
            return false;
        }

        public bool CastTo<T>(out T target)
        {
            if (typeof(T) == typeof(string))
            {
                target = (T)(object)_value;
                return true;
            }
            target = default;
            return false;
        }

        public object ScriptVariable()
        {
            return this;
        }

        public bool Write(GH_IWriter writer)
        {
            writer.SetInt32("FormatVersion", 1);
            writer.SetString("Value", _value);
            return true;
        }

        public bool Read(GH_IReader reader)
        {
            int formatVersion = reader.GetInt32("FormatVersion");
            if (formatVersion != 1)
            {
                return false;
            }
            _value = reader.GetString("Value");
            return true;
        }
    }
}
