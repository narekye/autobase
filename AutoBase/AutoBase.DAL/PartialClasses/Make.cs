﻿using System;

namespace AutoBase.DAL.AutoBaseEntities
{
    public partial class Make : IConvertible
    {
        public long DumpsCount { get; set; }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
            
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return new object();
            // throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return 0;
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return 0;
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return 0;
        }
    }
}
