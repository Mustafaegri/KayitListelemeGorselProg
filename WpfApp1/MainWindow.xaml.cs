using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace datagridbindingtextbox
{

    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NFDVFLO;Initial Catalog=asd;Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

       
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from workistan", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            datagridenes.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void ekle_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd;

            if (name.Text != "" && surname.Text != "")
            {
                cmd = new SqlCommand("insert into workistan(name,surname,job) values(@name,@surname,@job)", con);
                con.Open();

                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@surname", surname.Text);
                cmd.Parameters.AddWithValue("@job", jobtext.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Başarıyla Eklendi");
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
        }

        private void ClearData()
        {
            name.Text = "";
            surname.Text = "";
            jobtext.Text = "";
        }

        private void datagridenes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void datagridenes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gridData = this.datagridenes.CurrentItem as DataRowView;
            if(gridData != null)
            {
                var item = (gridData as DataRowView).Row;
                name.Text = item["name"].ToString();
                surname.Text = item["surname"].ToString();
                jobtext.Text = item["job"].ToString();
                idtext.Text = item["id"].ToString();
            }
            

            
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void gridName_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void idtext_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NFDVFLO;Initial Catalog=asd;Integrated Security=True");
            SqlCommand cmd;
            if (name.Text != "" && surname.Text != "")
            {
                cmd = new SqlCommand("update workistan set name=@name,surname=@surname,job=@job where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", idtext.Text);
                cmd.Parameters.AddWithValue("@name", name.Text);
                cmd.Parameters.AddWithValue("@surname", surname.Text);
                cmd.Parameters.AddWithValue("@job", jobtext.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-NFDVFLO;Initial Catalog=asd;Integrated Security=True");
            SqlCommand cmd;
            if (idtext.Text != null)
            {
                cmd = new SqlCommand("delete workistan where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", idtext.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                LoadGrid();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select * from workistan order by name asc", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            datagridenes.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from workistan order by name desc", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            datagridenes.ItemsSource = dt.DefaultView;
            con.Close();
        }

    }

    internal class DataItem
    {

        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string job { get; set; }

    }
}