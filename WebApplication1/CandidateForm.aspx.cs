using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using Newtonsoft.Json.Linq;



namespace WebApplication1
{
    public partial class CandidateForm : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;

                SqlConnection con = new SqlConnection(mainconn);

                con.Open();        con.Open();
                string sqlquery = "select * from [dbo].[City_Master]";
                SqlDataAdapter adapter = new SqlDataAdapter(sqlquery, con);
               
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
                string selectQuery = @"
                    SELECT c.Sr_No, c.Candidate_Name, c.DOB, c.Age, cm.city_name,cm.City_Id AS City FROM candidate c INNER JOIN City_Master cm ON c.City_Id = cm.city_id";

                SqlDataAdapter sqladapter = new SqlDataAdapter(selectQuery, con);
                DataTable dt2 = new DataTable();
                sqladapter.Fill(dt2); // Fill a DataTable instead of DataSet

                // Set the DataSource and DataTextField properties for City column
                GridView1.DataSource = dt2;
                GridView1.DataBind();
              
            }




        }

        protected void TbDobTextChanged(object sender, EventArgs e)
        {

            try {
                DateTime date = Convert.ToDateTime(tb_dob.Text);
                //TimeSpan timeSpan = System.DateTime.Now.Subtract(date);
                int year, month, days;
                month = 12 * (DateTime.Now.Year - date.Year) + (DateTime.Now.Month - date.Month);
                if (DateTime.Now.Day < date.Day) {
                    month -= 1;
                    //days = DateTime.DaysInMonth(date.Day, month) - date.Day + DateTime.Now.Day;
                } else {
                    days = DateTime.Now.Day - date.Day;
                }
                year = (month / 12);
                month -= year * 12;
                tb_age.Text = year.ToString();
            }
            catch {
                Exception exception = new Exception();
                tb_age.Text += exception.Message;
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            tb_cname.Text = "";
            tb_dob.Text = "";
            tb_age.Text = ""; // Assuming you want to clear the age field too
            DropDownList1.SelectedIndex = 0; // Select the first item in the dropdown list (assuming it's empty)
            
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridView1.EditIndex = e.NewEditIndex;  // Set edit index
            //GridViewRow row = GridView1.Rows[e.NewEditIndex];

            //if (row.RowType == DataControlRowType.DataRow)
           // {

              
                //DropDownList ddlCities = ((DropDownList)FindControl("ddlCities")) as DropDownList;

                //if (ddlCities == null)
                //{
                   // string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;

                  //  SqlConnection con = new SqlConnection(mainconn);

                   // con.Open();
                   // string sqlquery = "select * from [dbo].[City_Master]";
                   // SqlDataAdapter adapter = new SqlDataAdapter(sqlquery, con);
                   // DataTable dt2 = new DataTable();
                   // adapter.Fill(dt2);
                  //   Label messageLabel = new Label();
                   //  messageLabel.Text = " ddlCities : " + dt2.ToString();
                   //form1.Controls.Add(messageLabel);
                    //ddlCities.DataSource = dt;
                    //ddlCities.DataValueField = "city_id";
                    //ddlCities.DataTextField = "city_name";
                    //ddlCities.DataBind();
                //}
                //else
                //{
                //    Console.WriteLine("Dropdown NOT found.");
                //    Label messageLabel = new Label();
                //    messageLabel.Text = " Dropdown NOT found. ";
                //    form1.Controls.Add(messageLabel);
                //}
           // }
         

            BindGrid();  // Re-bind data to show editing controls
            
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Your code to handle GridView row updating logic here

            // Assuming TextBoxes are outside the GridView

            GridViewRow row = GridView1.Rows[e.RowIndex];
            //Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_Sr_No") as Label;
            TextBox name = GridView1.Rows[e.RowIndex].FindControl("txt_Candidate_Name") as TextBox;
            TextBox city = GridView1.Rows[e.RowIndex].FindControl("txt_City") as TextBox;
            TextBox dob = GridView1.Rows[e.RowIndex].FindControl("txt_DOB") as TextBox;
            TextBox age = GridView1.Rows[e.RowIndex].FindControl("txt_Age") as TextBox;

            // Get edited values from TextBoxes outside the GridView
            int candidateId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value); // Get ID from DataKey
            string candidateName = name.Text; // Assuming these TextBoxes exist outside GridView
          
            DateTime candidateDob = Convert.ToDateTime(dob.Text);

            string candidateAge = age.Text;
            string cityName = city.Text;
             CityUpdate (cityName);
             //Update database
              int rowsAffected = UpdateCandidate(candidateId, candidateName, candidateDob, candidateAge);

             if (rowsAffected > 0)
            {
            // Re-bind data to reflect changes
               GridView1.EditIndex = -1;
                 BindGrid();
             }
             else
              {
            // Handle the case where no rows were affected (e.g., display an error message)
             }

            // Display success message (optional)
            Label messageLabel = new Label();
            messageLabel.Text = "Candidate ID:" + candidateId.ToString() + " candidateName: " + candidateName + " candidateDob" + candidateDob.ToString() + "candidateAge " + candidateAge.ToString() + "selectedCityId " + city.Text.ToString();
            form1.Controls.Add(messageLabel);
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;  // Disable editing mode
            BindGrid();
            Response.Redirect("CandidateForm.aspx");
        }
        private void BindGrid()
        {
            string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(mainconn))
            {
                con.Open();

                // Select query (assuming you want to include Age in the grid)
                string selectQuery = @" SELECT c.Sr_No, c.Candidate_Name, c.DOB, c.Age, cm.city_name AS City FROM candidate c INNER JOIN City_Master cm ON c.City_Id = cm.city_id";

                SqlDataAdapter sqladapter = new SqlDataAdapter(selectQuery, con);
                DataTable dt = new DataTable();
                sqladapter.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private int UpdateCandidate(int candidateId, string candidateName, DateTime dob, string candidateAge)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(mainconn))
            {
                con.Open();

                string updateQuery = @"UPDATE candidate SET Candidate_Name = @Candidate_Name, DOB = @dob, Age = @age WHERE Sr_No = @id";
                SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                updateCommand.Parameters.AddWithValue("@name", candidateName);
                updateCommand.Parameters.AddWithValue("@dob", dob);
                updateCommand.Parameters.AddWithValue("@age", candidateAge);
                //updateCommand.Parameters.AddWithValue("@city_name", cityName);
                updateCommand.Parameters.AddWithValue("@id", candidateId);

                return updateCommand.ExecuteNonQuery();  // Return the number of affected rows
            }
            //Response.Redirect("CandidateForm.aspx");

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string candidateName = tb_cname.Text;
            DateTime dob;
            int candidateAge;
            int selectedCityId;

            // Attempt to parse values, handling potential nulls
            if (!DateTime.TryParse(tb_dob.Text, out dob))
            {
                // Handle invalid DOB format or null value
                // e.g., display an error message or set a default value
                dob = DateTime.MinValue; // Example default value
            }

            if (!int.TryParse(tb_age.Text, out candidateAge))
            {
                // Handle invalid age format or null value
                // e.g., display an error message or set a default value
                candidateAge = 0; // Example default value
            }

            if (!int.TryParse(DropDownList1.SelectedValue, out selectedCityId))
            {
                // Handle invalid city selection or null value
                // e.g., display an error message or set a default value
                selectedCityId = 0; // Example default value
            }

            try
            {
               // string candidateName = tb_cname.Text;
               // DateTime dob = Convert.ToDateTime(tb_dob.Text);
                //string candidateAge = tb_age.Text;

                // Create connection and open it (assuming connection string is already defined)
                string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(mainconn);
                con.Open();
                // Insert data into candidate table
                string insertQuery = @"INSERT INTO candidate (Candidate_Name, DOB, Age, City_Id) VALUES (@name, @dob, @age, @cityId)";
                SqlCommand insertCommand = new SqlCommand(insertQuery, con);
                insertCommand.Parameters.AddWithValue("@name", candidateName);
                insertCommand.Parameters.AddWithValue("@dob", dob );
                insertCommand.Parameters.AddWithValue("@age", candidateAge);
                // Get the selected city id from the dropdown list
                //int selectedCityId = int.Parse(DropDownList1.SelectedValue);
                //insertCommand.Parameters.AddWithValue("@cityId", selectedCityId);
                insertCommand.ExecuteNonQuery();


                // Display success message (optional)
                Label messageLabel = new Label();
                messageLabel.Text = "Candidate information saved successfully!" + selectedCityId.ToString();
                form1.Controls.Add(messageLabel);
            }
           // catch (SqlException ex)
            //{
                // Handle potential SQL errors (e.g., display error message to user)
             //   Label errorLabel = new Label();
             //   errorLabel.Text = "Error saving candidate: " + ex.Message;
             //   form1.Controls.Add(errorLabel);
          //  }
            catch (Exception ex)
            {
                // Handle potential SQL errors (e.g., display error message to user)
                Label errorLabel = new Label();
                errorLabel.Text = "Error saving candidate: " + ex.Message;
                form1.Controls.Add(errorLabel);
            }
            finally
            {
                // Close connection
                //con.Close();

                BindGrid();
            }
            
        }
        protected void Gridview1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(mainconn);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete FROM candidate where Sr_No='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            BindGrid();
        }

        protected void CityUpdate(string cityName)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["CadidatedbConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(mainconn);
            con.Open();
            // Insert data into candidate table
            string insertQuery = @"INSERT INTO City_Master (city_name) VALUES (@name)";
            SqlCommand insertCommand = new SqlCommand(insertQuery, con);
            insertCommand.Parameters.AddWithValue("@name", cityName);

            //insertCommand.Parameters.AddWithValue("@cityId", selectedCityId);
            insertCommand.ExecuteNonQuery();


            // Display success message (optional)
            Label messageLabel = new Label();
            messageLabel.Text = "Candidate information saved successfully!" + cityName;
            form1.Controls.Add(messageLabel);



        }
    }

 }
