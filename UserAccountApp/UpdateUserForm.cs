using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using UserAccountApp.Data;
using UserAccountApp.Models;

namespace UserAccountApp
{
    public partial class UpdateUserForm : Form
    {
        private int _userId; // Define the _userId field to store the UserId

        // Constructor that accepts the userId as a parameter
        public UpdateUserForm(int userId)
        {
            InitializeComponent();
            _userId = userId; // Assign the passed userId to the _userId field
        }

        private void UpdateUserForm_Load(object sender, EventArgs e)
        {
            LoadUserDetails(); // Load the user details when the form loads
        }

        // Method to load user details based on the _userId
        private void LoadUserDetails()
        {
            try
            {
                using (var context = new UserAccountDbContext())
                {
                    var user = context.Users
                                      .Include(u => u.Address)  // Ensure to include Address with the user
                                      .FirstOrDefault(u => u.UserId == _userId); // Use _userId to fetch the user
                    if (user != null)
                    {
                        // Populate the textboxes with the user's details
                        txtFname.Text = user.FullName;
                        txtUname.Text = user.UserName;
                        txtEmail.Text = user.Email;
                        txtPassword.Text = user.Password;
                        txtCity.Text = user.Address.City;
                        txtRegion.Text = user.Address.Region;
                        txtStreet.Text = user.Address.Street;
                        txtCountry.Text = user.Address.Country;
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading user details: " + ex.Message);
            }
        }

        // Method to save updated user details
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new UserAccountDbContext())
                {
                    var user = context.Users
                                      .Include(u => u.Address)  // Include the address
                                      .FirstOrDefault(u => u.UserId == _userId); // Use _userId here to find the user
                    if (user != null)
                    {
                        // Update the user fields with the new values from the textboxes
                        user.FullName = txtFname.Text;
                        user.UserName = txtUname.Text;
                        user.Email = txtEmail.Text;
                        user.Password = txtPassword.Text;

                        // Update the user's address
                        user.Address.City = txtCity.Text;
                        user.Address.Region = txtRegion.Text;
                        user.Address.Street = txtStreet.Text;
                        user.Address.Country = txtCountry.Text;

                        // Save the changes to the database
                        context.SaveChanges();
                        MessageBox.Show("User updated successfully.");
                        this.Close(); // Close the form after saving the updates
                    }
                    else
                    {
                        MessageBox.Show("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the user: " + ex.Message);
            }
        }
    }
}
