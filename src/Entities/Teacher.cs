#region ENBREA - Copyright (C) 2020 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA
 *    
 *    Copyright (C) 2020 STÜBER SYSTEMS GmbH
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
    /// An entity within the SchildNRW database table "k_lehrer"
    /// </summary>
    public class Teacher
    {
        public DateTime? Birthdate { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public Gender? Gender { get; set; }
        public int Id { get; set; }
        public string Lastname { get; set; }
        public DateTime? LeaveDate { get; set; }
        public string Locality { get; set; }
        public string Mobile { get; set; }
        public string OfficialEmail { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string Salutation { get; set; }
        public DateTime? StartDate { get; set; }
        public string Street { get; set; }
        public string Title { get; set; }

        public static Teacher FromDb(DbDataReader reader)
        {
            return new Teacher
            {
                Id = reader.GetValue<int>("ID"),
                Code = reader.GetValue<string>("Kuerzel"),
                Lastname = reader.GetValue<string>("Nachname"),
                Firstname = reader.GetValue<string>("Vorname"),
                Salutation = reader.GetValue<string>("Anrede"),
                Title = reader.GetValue<string>("Titel"),
                Birthdate = reader.GetValue<DateTime?>("Geburtsdatum"),
                Gender = reader.GetGenderValue("Geschlecht"),
                Email = reader.GetValue<string>("EMail"),
                Street = reader.GetValue<string>("Strasse"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("Ort"),
                Region = reader.GetValue<string>("Kreis"),
                Country = reader.GetValue<string>("Land"),
                Phone = reader.GetValue<string>("Tel"),
                Mobile = reader.GetValue<string>("Handy"),
                StartDate = reader.GetValue<DateTime?>("DatumZugang"),
                LeaveDate = reader.GetValue<DateTime?>("DatumAbgang"),
                OfficialEmail = reader.GetValue<string>("EMailDienstlich"),
            };
        }
    }
}
