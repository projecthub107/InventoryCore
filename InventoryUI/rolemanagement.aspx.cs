using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rolemanagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["oUser"] == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["LoginPage"].ToString());
            }

            if (Page.User.IsInRole("sett009") == false)
            {
                // No Permission Page.
                Response.Redirect("nopermission.aspx");
            }

            BindRoles();
            RefreshData(Convert.ToInt32(ddlRoles.SelectedValue));
        }
    }
    private void BindRoles()
    {
        DataClassesDataContext _db = new DataClassesDataContext();
        var roles = from ro in _db.Roles
                    where ro.Status == 1
                    select ro;
        ddlRoles.DataSource = roles;
        ddlRoles.DataTextField = "Name";
        ddlRoles.DataValueField = "RoleId";
        ddlRoles.DataBind();
    }
    private void RefreshData(int nRoleId)
    {
        LoadTree();
        DataClassesDataContext _db = new DataClassesDataContext();
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        var item = _db.RoleRights.Where(rr => rr.RoleId == nRoleId && rr.ClientId == nClientId);
        int cnt = 0;
        foreach (TreeNode node in trvMenu.Nodes)
        {
            node.Checked = false;
            if (node.ChildNodes.Count > 0)
            {
                cnt = 0;
                foreach (TreeNode subNode in node.ChildNodes)
                {
                    foreach (RoleRight rr in item)
                    {
                        if (Convert.ToInt32(subNode.Value) == rr.MenuId)
                        {
                            subNode.Checked = true;
                            cnt++;
                        }
                    }

                    if (cnt == 0)
                        node.Checked = false;
                    else
                        node.Checked = true;
                }
            }
        }
        trvMenu.ExpandAll();
    }
    private void AddChildMenu(TreeNode parentNode)
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();
        var items = _db.MenuItems.Where(mi => mi.ClientId == nClientId && mi.Status == 1).ToList();
        foreach (MenuItem objMenu in items)
        {
            if (objMenu.ParentId.ToString() == parentNode.Value)
            {
                TreeNode node = new TreeNode(objMenu.Name, objMenu.MenuId.ToString());
                parentNode.ChildNodes.Add(node);
            }
        }
    }
    private void LoadTree()
    {
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        DataClassesDataContext _db = new DataClassesDataContext();
        MenuItem objMenus = new MenuItem();
        var items = _db.MenuItems.Where(mi => mi.ClientId == nClientId && mi.Status == 1).ToList();

        trvMenu.Nodes.Clear();
        foreach (MenuItem objMenu in items)
        {
            if (objMenu.ParentId == 0)
            {
                TreeNode node = new TreeNode(objMenu.Name, objMenu.MenuId.ToString());
                trvMenu.Nodes.Add(node);
                AddChildMenu(node);
            }
            else
            {
                //    dtMenu.Rows.Add(new object[] { objMenu.MenuID, objMenu.MenuName, objMenu.ParentID });
            }
        }
    }
    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResult.Text = "";
        RefreshData(Convert.ToInt32(ddlRoles.SelectedValue));
        //CheckExistingUIs(Convert.ToInt32(ddlRoles.SelectedValue));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblResult.Text = "";
        string test = "";
        DataClassesDataContext _db = new DataClassesDataContext();

        int nRoleId = Convert.ToInt32(ddlRoles.SelectedValue);
        int nClientId = Convert.ToInt32(ConfigurationManager.AppSettings["client_id"]);
        string strQ = "DELETE RoleRight WHERE RoleId=" + nRoleId + " AND ClientId=" + nClientId;
        _db.ExecuteCommand(strQ, string.Empty);
        _db.SubmitChanges();

        try
        {
            bool bFound = false;
            foreach (TreeNode node in trvMenu.Nodes)
            {
                if (node.Text == "Settings")
                {
                    //
                }

                bFound = false;
                if (node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode subNode in node.ChildNodes)
                    {
                        test += subNode.Text + ", ";
                        if (subNode.Checked)
                        {
                            bFound = true;
                            RoleRight obj = new RoleRight();
                            obj.MenuId = Convert.ToInt32(subNode.Value);
                            obj.ClientId = nClientId;
                            obj.RoleId = nRoleId;
                            _db.RoleRights.InsertOnSubmit(obj);
                        }
                    }
                    if (bFound)
                    {
                        test += node.Text + ", ";
                        RoleRight obj = new RoleRight();
                        obj.MenuId = Convert.ToInt32(node.Value);
                        obj.ClientId = nClientId;
                        obj.RoleId = nRoleId;
                        _db.RoleRights.InsertOnSubmit(obj);
                    }
                }
            }
            _db.SubmitChanges();
            lblResult.Text = utility.GetSystemMessage("Data saved successfully");

        }
        catch (Exception ex)
        {
            var t = test;
            lblResult.Text = utility.GetSystemErrorMessage(ex.Message);

        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void trvMenu_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        TreeView tv = (TreeView)sender;
        TreeNode selectedNode = trvMenu.SelectedNode;
    }
}