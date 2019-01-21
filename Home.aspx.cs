using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;

namespace WebFormsApplication
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Literal1.Text += "<div class='row'>";

                SqlConnection con = new SqlConnection();

                DatabaseConnection.OpenConnection(con);

                liSta.Visible = new Feature1Toggle().FeatureEnabled;

                liEle.Visible = new Feature2Toggle().FeatureEnabled;

                SqlCommand cmd = null;
                SqlDataReader dr = null;

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
                int iCounter = 0;
                while (dr.Read())
                {
                    Literal1.Text += "<div class='col-sm-4'>";
                    Literal1.Text += "<div class='panel panel-primary'>";
                    Literal1.Text += "<div class='panel-heading'>" + dr[1] + "     - Price : Rs. "+ dr[2] + "</div>";
                    Literal1.Text += "<div class='panel-body'>";
                    //Literal1.Text += "<img src='" + dr[3] + "' class='img-responsive' style='width:100%' alt='Image'>";
                    //Literal1.Text += "</div>";
                    //Literal1.Text += "<div class='panel-footer'>&#8377;" + dr[2] + "&nbsp;";
                    Literal1.Text += "<input id = 'hdn" + dr[0] + "' type = 'hidden' value = '" + dr[2] + "' />";
                    Literal1.Text += "<div>Quantity" + "&nbsp;";
                    Literal1.Text += "<input type = 'number' id= 'txt" + dr[0] + "' runat='server' min ='1' max ='100' style='width: 50px;' onchange ='myFunction(this.value,"+ dr[0] + ")'>";
                    Literal1.Text += "&nbsp;&nbsp;&nbsp;";
                    Literal1.Text += "<label>Rs. </label>";
                    Literal1.Text += "<label  class='lbl' id='lbl" + dr[0] + "' runat='server'/>";
                    Literal1.Text += "</div>";
                    Literal1.Text += "</div>";
                    //Literal1.Text += "<a href='ProductDetails.aspx?pid=" + dr[0] + "'></a>" + "</div>";
                    Literal1.Text += "</div>";
                    Literal1.Text += "</div>";
                    iCounter++;
                }
                dr.Close();

                cmd.Dispose();

                DatabaseConnection.CloseConnection(con);
                Literal1.Text += "</div>";
            }
        }

        protected async void btnOrderNow_Click(object sender, EventArgs e)
        {
            
            string amount = hdnTotal.Value.ToString();
            string x= await  ValidateAmount(amount);
            Literal2.Text += "<center style='margin-bottom:25px; font-weight:700'>"+  x + "</center>";
           
            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert("+ x +")", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + x + "');", true);
        }

        private async Task<string> ValidateAmount(string amount)
        {

            string azureBaseUrl = "https://servicefunctocalculatetotal20190120085544.azurewebsites.net/api/CheckTotal?Name="+ amount;
            //string urlQueryStringParams = $"?name={amount}";
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //var content = new FormUrlEncodedContent(new[]
            //{
            //     new KeyValuePair<string, string>("Name", amount)
            //});
            

            using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsync(azureBaseUrl, null))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = content.ReadAsStringAsync().Result;
                            if (data != null)
                            {
                                return data;
                            }
                            else
                                return "";
                        }
                    }
            }
        }
    }
}