#region Enbrea - Copyright (C) STÜBER SYSTEMS GmbH
/*    
 *    Enbrea
 *    
 *    Copyright (C) STÜBER SYSTEMS GmbH
 *
 *    This program is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU Affero General Public License, version 3,
 *    as published by the Free Software Foundation.
 *
 *    This program is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *    GNU Affero General Public License for more details.
 *
 *    You should have received a copy of the GNU Affero General Public License
 *    along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 */
#endregion

using System;
using System.Data.Common;

namespace Enbrea.SchildNRW.Db
{
    /// <summary>
    /// An entity within the SchildNRW database table "schueler"
    /// </summary>
    public class Student
    {
        public DateTime? Birthdate { get; set; }
        public string Birthname { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string Firstname { get; set; }
        public Gender? Gender { get; set; }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string Locality { get; set; }
        public string Nationality1 { get; set; }
        public string Nationality2 { get; set; }
        public string PlaceOfBirth { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string SchoolClass { get; set; }
        public short SchoolLevel { get; set; }
        public StudentStatus? Status { get; set; }
        public string Street { get; set; }

        public static Student FromDb(DbDataReader reader)
        {
            return new Student
            {
                Id = reader.GetValue<int>("ID"),
                Lastname = reader.GetValue<string>("Name"),
                Firstname = reader.GetValue<string>("Vorname"),
                Street = reader.GetValue<string>("Strasse"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("Ort"),
                Region = reader.GetValue<string>("Kreis"),
                Country = reader.GetValue<string>("Land"),
                Email = reader.GetValue<string>("SchulEmail"),
                Birthdate = reader.GetValue<DateTime?>("Geburtsdatum"),
                Gender = reader.GetGenderValue("Geschlecht"),
                SchoolClass = reader.GetValue<string>("Klasse"),
                EnrollmentDate = reader.GetValue<DateTime?>("Aufnahmedatum"),
                LeaveDate = reader.GetValue<DateTime?>("Entlassdatum"),
                Status = reader.GetStatusValue("Status"),
                Birthname = reader.GetValue<string>("Geburtsname"),
                PlaceOfBirth = reader.GetValue<string>("Geburtsort"),
                SchoolLevel = reader.GetValue<short>("Jahrgang"),
                Nationality1 = reader.GetValue<string>("StaatKrz"),
                Nationality2 = reader.GetValue<string>("StaatKrz2")
            };
        }
    }
}
