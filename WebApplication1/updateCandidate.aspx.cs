using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace WebApplication1
{
    public partial class updateCandidate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;

                SqlConnection con = new SqlConnection(mainconn);

                con.Open();
                string sqlquery = "select * from [dbo].[City_Master]";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlquery, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DropDownList1.DataSource = dt;
                DropDownList1.DataValueField = "city_id";
                DropDownList1.DataTextField = "city_name";
                DropDownList1.DataBind();

                /* string sqlquery1 = "SELECT [dbo].[Candidate_basic_Detail].candidate_id,[dbo].[Candidate_basic_Detail].candidate_name,[dbo].[Candidate_basic_Detail].DOB,[dbo].[Candidate_basic_Detail].Age,[dbo].[City_Master].city_name FROM [dbo].[Candidate_basic_Detail] INNER JOIN [dbo].[City_Master] ON [dbo].[Candidate_basic_Detail].City_Id = [dbo].[City_Master].city_id";
                SqlDataAdapter sqladapter = new SqlDataAdapter( sqlquery1, con);
                DataSet ds = new DataSet();
                sqladapter.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                */

                // Create table if not exists
                //string checkTableQuery = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'candidate'";
                //string createTableQuery = @"
                //    CREATE TABLE candidate (
                //        Sr_No INT IDENTITY(1, 1) PRIMARY KEY,
                //        Candidate_Name VARCHAR(255) NOT NULL,
                //        DOB DATE,
                //        Age INT,
                //        City_Id INT FOREIGN KEY REFERENCES City_Master(city_id)
                //    );
                //";


                //using (SqlCommand checkTableCommand = new SqlCommand(checkTableQuery, con))

                //using (SqlDataReader reader = checkTableCommand.ExecuteReader())
                //{
                //    if (!reader.HasRows) // Check if reader has any rows (meaning table doesn't exist)
                //    {
                //        // Table doesn't exist, create it
                //        SqlCommand createTableCommand = new SqlCommand(createTableQuery, con);
                //        createTableCommand.ExecuteNonQuery();
                //    }
                //    reader.Close();
                //}



                // Now fetch data from the table
                //string selectQuery = "SELECT * FROM candidate";
                //string selectQuery = @"
                //SELECT c.Sr_No, c.Candidate_Name, c.DOB, c.Age, cm.city_name,cm.City_Id AS City FROM candidate c INNER JOIN City_Master cm ON c.City_Id = cm.city_id";

                //SqlDataAdapter sqladapter = new SqlDataAdapter(selectQuery, con);
                //DataTable dt2 = new DataTable();
                //sqladapter.Fill(dt2); // Fill a DataTable instead of DataSet

                //// Set the DataSource and DataTextField properties for City column
                //GridView1.DataSource = dt2;
                //GridView1.DataBind();


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}