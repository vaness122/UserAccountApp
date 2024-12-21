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
    }
}
