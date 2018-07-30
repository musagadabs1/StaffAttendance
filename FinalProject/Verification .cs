using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FinalProject
{
    class Verification
    {
        public static void Input()
        {
            MessageBox.Show("Please fill all the fields", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void alreadyExist()
        {
            MessageBox.Show("CNIC already exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Save()
        {
            MessageBox.Show("Record Successfully saved","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void Duplicate()
        {
            MessageBox.Show("CNIC Already exist", "Duplicate...", MessageBoxButtons.OK, MessageBoxIcon.Stop);            
        }
        public static void Update()
        {
            MessageBox.Show("Record Successfully Updated","Update",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void Delete()
        {
            MessageBox.Show("Record Successfully Deleted","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public static void PasswordMatch()
        {
            MessageBox.Show("Both password Does not match","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        public static void InvalidUser()
        {
            MessageBox.Show("Invalid ID Or Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        public static void Gender()
        {
            MessageBox.Show("Please select Gender","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
        public static void AccountStatus()
        {
            MessageBox.Show("Please select Account status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void Picture()
        {
            MessageBox.Show("Please browse Picture", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void time()
        {
            MessageBox.Show("Invalid entry\n Time In must be early to the Time out", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
