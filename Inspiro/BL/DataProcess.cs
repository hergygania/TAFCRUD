﻿using Inspiro.Helper;
using Inspiro.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inspiro.BL
{
    public class DataProcess : BaseDA
    {
        public List<KaryawanModel> GetAll(ref string msg)
        {
            var response = new List<KaryawanModel>();
            var query = string.Format(@"SELECT * FROM KARYAWAN");
            var result = ExecuteQueryWithParam(query, new List<SqlParameter>(), ref msg);

            if (msg.Length > 0)
            {
                return response;
            }

            if (result.Rows.Count > 0)
            {
                response = DataTableHelper.DataTableToList<KaryawanModel>(result);
            }

            return response;
        }

        public bool Insert(KaryawanModel data, ref string msg)
        {
            string time = DateTime.Now.ToString("hh:mm:ss tt");
            var dateGabung = data.Bulan + "/" + data.Date + "/" + data.year + " " + time;
            var fixDate = dateGabung.Replace('.', ':');
            //var dateJadi = DateTime.Parse(dateGabung);
            var query = string.Format(@"
                INSERT INTO KARYAWAN (Nama, NIK, Posisi, Tgl_Lahir, Divisi, JenisKelamin)
                VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}')
            ", data.Nama, data.NIK, data.Posisi, fixDate, data.Divisi, data.JenisKelamin);
            var result = ExecuteQueryWithParam(query, new List<SqlParameter>(), ref msg);

            if (msg.Length > 0)
            {
                return false;
            }

            return true;
        }
    }
}
