using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace online_student_registration_system
{
    public partial class StudentRegistrationForm : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["StudentDb"]?.ConnectionString;
        private int selectedStudentId = -1;

        public StudentRegistrationForm()
        {
            InitializeComponent();
        }

        private void StudentRegistrationForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Database connection is not configured. Please update App.config before running the application.",
                    "Configuration Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("SELECT id, stname, course, fee FROM student ORDER BY id", connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    dataGridView2.Rows.Clear();

                    foreach (DataRow row in table.Rows)
                    {
                        dataGridView2.Rows.Add(row["id"], row["stname"], row["course"], row["fee"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load student records. " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text.Trim();
            string course = textBox2.Text.Trim();
            string feeText = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(course) || string.IsNullOrWhiteSpace(feeText))
            {
                MessageBox.Show("Please enter the student name, course and fee.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal fee;
            if (!decimal.TryParse(feeText, out fee) || fee < 0)
            {
                MessageBox.Show("Please enter a valid non-negative fee.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("Database connection is not configured. Please update App.config before running the application.",
                    "Configuration Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = connection.CreateCommand())
                {
                    if (selectedStudentId < 0)
                    {
                        command.CommandText = "INSERT INTO student (stname, course, fee) VALUES (@stname, @course, @fee)";
                    }
                    else
                    {
                        command.CommandText = "UPDATE student SET stname = @stname, course = @course, fee = @fee WHERE id = @id";
                        command.Parameters.Add("@id", SqlDbType.Int).Value = selectedStudentId;
                    }

                    command.Parameters.Add("@stname", SqlDbType.NVarChar, 100).Value = name;
                    command.Parameters.Add("@course", SqlDbType.NVarChar, 100).Value = course;
                    command.Parameters.Add("@fee", SqlDbType.Decimal).Value = fee;
                    command.Parameters["@fee"].Precision = 18;
                    command.Parameters["@fee"].Scale = 2;

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show(selectedStudentId < 0 ? "Student added successfully." : "Student updated successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The student record could not be saved. " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || dataGridView2.Rows[e.RowIndex].Cells[0].Value == null)
                return;

            int id = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value);

            if (e.ColumnIndex == dataGridView2.Columns["Column5"].Index)
            {
                selectedStudentId = id;
                textBox3.Text = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
                textBox2.Text = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[2].Value);
                textBox1.Text = Convert.ToString(dataGridView2.Rows[e.RowIndex].Cells[3].Value);
                button4.Text = "Update";
            }
            else if (e.ColumnIndex == dataGridView2.Columns["Column6"].Index)
            {
                if (MessageBox.Show("Delete this student record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                DeleteStudent(id);
            }
        }

        private void DeleteStudent(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("DELETE FROM student WHERE id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                ClearForm();
                LoadStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The student record could not be deleted. " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            textBox3.Clear();
            textBox2.Clear();
            textBox1.Clear();
            selectedStudentId = -1;
            button4.Text = "Save";
            textBox3.Focus();
        }

        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
