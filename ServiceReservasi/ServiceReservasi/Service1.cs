﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceReservasi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string koneksi = "Data Source=LAPTOP-K3US2KD2;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID= sa; Password=semangatlulus";
        SqlConnection conn;
        SqlCommand comm;

        public List<DetailLokasi> DetailLokasi()
        {
            List<DetailLokasi> LokasiFull = new List<DetailLokasi>();
            try
            {
                string sql = "SELECT ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi";
                conn = new SqlConnection(koneksi);
                comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    DetailLokasi data = new DetailLokasi();
                    data.IDLokasi = reader.GetString(0);
                    data.NamaLokasi = reader.GetString(1);
                    data.DeskripsiFull = reader.GetString(2);
                    data.Kuota = reader.GetInt32(3);
                    LokasiFull.Add(data);

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return LokasiFull;
        }

        public List<Pemesanan> Pemesanan()
        {
            List<Pemesanan> pemesanans = new List<Pemesanan>();
            try
            {
                string sql = "SELECT ID_reservasi, Nama_customer, No_telpon, Jumlah_pemesanan, Nama_Lokasi FROM dbo.Pemesanan p join dbo. p.ID_lokasi = l.ID_lokasi";
                conn = new SqlConnection(koneksi);
                comm = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Pemesanan data = new Pemesanan();
                    data.IDPemesanan = reader.GetString(0);
                    data.NamaCustomer = reader.GetString(1);
                    data.NoTelpon = reader.GetString(2);
                    data.JumlahPemesanan = reader.GetInt32(3);
                    data.Lokasi = reader.GetString(4);
                    pemesanans.Add(data);

                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return pemesanans;
        }

        public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
        {
            string n = "gagal";
            try
            {
                string sql = "INSERT INTO dbo.Pemesanan VALUES('" + IDPemesanan + "','" + NamaCustomer + "','" + NoTelpon + "'," + JumlahPemesanan + ",'" + IDLokasi + "')";
                conn = new SqlConnection(koneksi);
                comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();

                string sql2 = "UPDATE dbo.Lokasi set Kuota = Kuota - " + JumlahPemesanan + " WHERE ID_lokasi = '" + IDLokasi + "' ";
                conn = new SqlConnection(koneksi);
                comm = new SqlCommand(sql2, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();

                n = "Berhasil";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return n;
        }

        public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
        {
            string n = "gagal";
            try
            {
                string sql = "UPDATE dbo.Pemesanan SET Nama_customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "' WHERE ID_reservasi = '" + IDPemesanan + "' ";
                conn = new SqlConnection(koneksi);
                comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();

                n = "Berhasil";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return n;
        }

        public string deletePemesanan(string IDPemesanan)
        {
            string n = "gagal";
            try
            {
                string sql = "DELETE FROM dbo.Pemesanan WHERE ID_reservasi = '" + IDPemesanan + "' ";
                conn = new SqlConnection(koneksi);
                comm = new SqlCommand(sql, conn);
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();

                n = "Berhasil";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return n;
        }

        public List<CekLokasi> ReviewLokasi()
        {
            throw new NotImplementedException();
        }
    }
}