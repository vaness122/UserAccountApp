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
using UserAccountApp.Models;

namespace UserAccountApp
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new User
                {
                    FullName = txtFname.Text,
                    UserName = txtUname.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text
                };

                var address = new Address
                {
                    City = txtCity.Text,
                    Region = txtRegion.Text,
                    Street = txtStreet.Text,
                    Country = txtCountry.Text
                };

                using (var context = new UserAccountDbContext())
                {
                    // Ensure the address is linked to the user (optional if there's a foreign key)
                    user.Address = address; // Assuming the User model has an Address property or set the foreign key

                    context.Users.Add(user);
                    context.Addresses.Add(address); // Add this if Address is not already linked to the user.

                    // Save the changes to the database
                    context.SaveChanges();
                }

                MessageBox.Show("User and Address Registered Successfully");
            }
            catch (Exception ex)
            {
                // Display the error message
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
