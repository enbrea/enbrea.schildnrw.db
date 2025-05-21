#region ENBREA SCHILD-NRW.DB - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    Enbrea SCHILD-NRW.DB
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using System;
using System.Data.Common;

namespace Enbrea.SchildNRW.Db
{
    /// <summary>
    /// Extensions for <see cref="DbDataReader"/>
    /// </summary>
    public static class DbDataReaderExtensions
    {
        public static Gender? GetGenderValue(this DbDataReader dbDataReader, string name)
        {
            var value = GetValue<short?>(dbDataReader, name);
            
            if (value != null)
            {
                return value switch
                {
                    3 => Gender.Male,
                    4 => Gender.Female,
                    _ => Gender.Unknown,
                };
            }
            else
            {
                return null;
            }
        }

        public static StudentStatus? GetStatusValue(this DbDataReader dbDataReader, string name)
        {
            var value = GetValue<short?>(dbDataReader, name);

            if (value != null)
            {
                return value switch
                {
                    0 => StudentStatus.NewAdmission,
                    1 => StudentStatus.OnWaitingList,
                    2 => StudentStatus.Active,
                    3 => StudentStatus.LeaveOfAbsence,
                    6 => StudentStatus.External,
                    8 => StudentStatus.HighSchool,
                    9 => StudentStatus.Graduate,
                    _ => null,
                };
            }
            else
            {
                return null;
            }
        }

        public static TValue GetValue<TValue>(this DbDataReader dbDataReader, string name)
        {
            var value = dbDataReader[name];

            if (value != null)

            {
                var t = value.GetType();

                if (t == typeof(DBNull))
                {
                    return default;
                }
                else
                {
                    var convertionType = Nullable.GetUnderlyingType(typeof(TValue));
                    if (convertionType == null)
                    {
                        convertionType = typeof(TValue);
                    }
                    return (TValue)Convert.ChangeType(value, convertionType);
                }
            }
            else
            {
                return default;
            }
        }
    }
}