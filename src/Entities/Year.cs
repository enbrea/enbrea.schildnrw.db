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
    /// An entity within the SchildNRW database table "eigeneschule_jahrgaenge"
    /// </summary>
    public class Year
    {
        public string ASDName { get; set; }
        public string ASDYear { get; set; }
        public int Id { get; set; }
        public string InternalCode { get; set; }
        public char? Visible { get; set; }

        public static Year FromDb(DbDataReader reader)
        {
            return new Year
            {
                Id = reader.GetValue<int>("ID"),
                InternalCode = reader.GetValue<string>("InternBez"),
                ASDYear = reader.GetValue<string>("ASDJahrgang"),
                ASDName = reader.GetValue<string>("ASDBezeichnung"),
                Visible = reader.GetValue<char?>("Sichtbar")
            };
        }
    }
}