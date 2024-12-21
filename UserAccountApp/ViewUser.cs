using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserAccountApp.Data;

namespace UserAccountApp
{
    public partial class ViewUser : Form
    {
        public ViewUser()
        {
            InitializeComponent();
        }

        private void ViewUser_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {

                using (var context = new UserAccountDbContext())
                {
                    var users = context.Users.ToList(); // Fetch all users from the Users table

                    // Bind the users list to the DataGridView
                    dataGridView1.DataSource = users;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred while loading users: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var userId = (int)selectedRow.Cells["UserId"].Value; 

                try
                {
                    using (var context = new UserAccountDbContext())
                    {
                        var user = context.Users.FirstOrDefault(u => u.UserId == userId);
                        if (user != null)
                        {
                            context.Users.Remove(user); // Remove the user
                            context.SaveChanges(); // Save changes to the database
                            LoadUsers(); // Reload users
                            MessageBox.Show("User deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the user: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var userId = (int)selectedRow.Cells["UserId"].Value; // Assuming "UserId" is the column name

                // Open a new form to update user details
                var updateForm = new UpdateUserForm(userId);
                updateForm.ShowDialog();

                // Reload the users after updating
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }
    }
    }

