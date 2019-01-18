﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebFormsApplication
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Literal1.Text += "<div class='row'>";

            SqlConnection con = new SqlConnection();

            DatabaseConnection.OpenConnection(con);

            liSta.Visible = new Feature1Toggle().FeatureEnabled;

            liEle.Visible = new Feature2Toggle().FeatureEnabled;

            SqlCommand cmd=null;
            SqlDataReader dr=null;

            if (new Feature1Toggle().FeatureEnabled)
            {
                liSta.Attributes.Add("class", "active");
                cmd = new SqlCommand("select * from Products where ProductType='S'", con);
                dr = cmd.ExecuteReader();
            }
            else if (new Feature2Toggle().FeatureEnabled)
            {
                liEle.Attributes.Add("class", "active");
                cmd = new SqlCommand("select * from Products where ProductType='E' ", con);
                dr = cmd.ExecuteReader();
            }

            while (dr.Read())
            {
                Literal1.Text += "<div class='col-sm-4'>";
                Literal1.Text += "<div class='panel panel-primary'>";
                Literal1.Text += "<div class='panel-heading'>" + dr[1] + "</div>";
                //Literal1.Text += "<div class='panel-body'>";
                //Literal1.Text += "<img src='" + dr[3] + "' class='img-responsive' style='width:100%' alt='Image'>";
                //Literal1.Text += "</div>";
                Literal1.Text += "<div class='panel-footer'>&#8377;" + dr[2] + "&nbsp;";
                Literal1.Text += "<div>Quantity" + "&nbsp;";
                Literal1.Text += "<input type = 'number' min = '1' max = '100'>";
                Literal1.Text += "</div>";
                Literal1.Text += "<a href='ProductDetails.aspx?pid=" + dr[0] + "' class='pull-right btn btn-primary' style='margin-top: -7px;'>Order Now</a></div>";
                Literal1.Text += "</div>";
                Literal1.Text += "</div>";
            }

            dr.Close();

            cmd.Dispose();

            DatabaseConnection.CloseConnection(con);
            Literal1.Text += "</div>";
        }
    }
}