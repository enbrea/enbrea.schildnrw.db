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

using System.Data.Common;

namespace Enbrea.SchildNRW.Db
{
    /// <summary>
    /// An entity within the SchildNRW database table "eigeneschule"
    /// </summary>
    public class MySchool
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Locality { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string SchoolNo { get; set; }
        public short SchoolTerm { get; set; }
        public string SchoolTermName { get; set; }
        public string SchoolTerm1Name { get; set; }
        public string SchoolTerm2Name { get; set; }
        public string SchoolTerm3Name { get; set; }
        public string SchoolTerm4Name { get; set; }
        public int SchoolTermCount { get; set; }
        public short SchoolYear { get; set; }
        public string Street { get; set; }
        public string Telefax { get; set; }
     
        public static MySchool FromDb(DbDataReader reader)
        {
            return new MySchool
            {
                Id = reader.GetValue<int>("ID"),
                SchoolNo = reader.GetValue<string>("SchulNr"),
                Name1 = reader.GetValue<string>("Bezeichnung1"),
                Name2 = reader.GetValue<string>("Bezeichnung2"),
                Name3 = reader.GetValue<string>("Bezeichnung3"),
                Street = reader.GetValue<string>("Strasse"),
                PostalCode = reader.GetValue<string>("PLZ"),
                Locality = reader.GetValue<string>("Ort"),
                Phone = reader.GetValue<string>("Telefon"),
                Telefax = reader.GetValue<string>("Fax"),
                Email = reader.GetValue<string>("EMail"),
                SchoolYear = reader.GetValue<short>("Schuljahr"),
                SchoolTerm = reader.GetValue<short>("SchuljahrAbschnitt"),
                SchoolTermCount = reader.GetValue<short>("AnzahlAbschnitte"),
                SchoolTermName = reader.GetValue<string>("AbschnittBez"),
                SchoolTerm1Name = reader.GetValue<string>("BezAbschnitt1"),
                SchoolTerm2Name = reader.GetValue<string>("BezAbschnitt2"),
                SchoolTerm3Name = reader.GetValue<string>("BezAbschnitt3"),
                SchoolTerm4Name = reader.GetValue<string>("BezAbschnitt4")
            };
        }
    }
}