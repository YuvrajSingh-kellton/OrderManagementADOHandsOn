using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace OrderMgntADO
{
    public class ItemMaster
    {
        private Double? ItemQuantity;
        private Double? ItemPrice;
        private string ItemName;
        private static readonly string _connectionString = @"Data Source=DESKTOP-0P56O06;Initial Catalog=NetFramework;Integrated Security=True";
        private readonly SqlConnection _connection = new SqlConnection(_connectionString);
        private SqlDataAdapter sqlDataAdapter;
        private DataTable getDataItem = new DataTable();

        private void AddItem()
        {
        AddMore:
            try
            {
                Console.WriteLine("Enter The Item name ");
                ItemName = Console.ReadLine();

                Console.WriteLine("Enter The Item Price");
                ItemPrice = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter The Item Quantity");
                ItemQuantity = Double.Parse(Console.ReadLine());
                if (ItemQuantity > 0 && ItemPrice > 0 && ItemName != "")
                {
                    if (!ItemAlreadyExistOrNot(ItemName))
                    {
                        string sql = "insert into OrderItem values('" + ItemName + "'," + ItemPrice + "," + ItemQuantity + ")";
                        sqlDataAdapter = new SqlDataAdapter(sql, _connection);
                        sqlDataAdapter.Fill(getDataItem);
                        Console.WriteLine("Item Added Successfully");
                        Console.WriteLine();
                        Console.WriteLine("Do you Want to Add More Items");
                        Console.WriteLine("Press : 1");
                        int check = Convert.ToInt32(Console.ReadLine());
                        if(check == 1)
                        {
                            goto AddMore;
                        }
                        else
                        {
                            return;
                        }
                            

                    }
                    else
                    {
                        Console.WriteLine("Item Already Exist ");
                        goto AddMore;
                    }

                }
                else
                {
                    Console.WriteLine(" Left blank Enter Item");
                    goto AddMore;
                }


            }
            catch (FormatException ex)
            {
                Console.WriteLine("please valid Constraints ");
                goto AddMore;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void DeleteItem()
        {
        DeleteItemAgain:
            try
            {

                Console.WriteLine("ENter the Item Name To be Delete");
                string deleteItemName = Console.ReadLine();
                if (deleteItemName == "")
                {
                    Console.WriteLine("Enter the Valid Name It Can't Be Null");
                    goto DeleteItemAgain;
                }
                else
                {
                    if (ItemAlreadyExistOrNot(deleteItemName))
                    {
                        string sql = "delete from OrderItem where ItemName='" + deleteItemName + "'";
                        sqlDataAdapter = new SqlDataAdapter(sql, _connection);
                        sqlDataAdapter.Fill(getDataItem);

                        Console.WriteLine();
                        Console.WriteLine();

                        Console.WriteLine("Item Delete SUccessfully !.");

                    }
                    else
                    {
                        Console.WriteLine("Item Does Not Exist in Record ...\n Enter Valid Name");
                        goto DeleteItemAgain;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }

        }
        private void UpdateItem()
        {
        UpdateItemAgain:
            getDataItem = null;
            string updateItemName;
            try
            {
                Console.WriteLine("Enter The Item name to Update");
                updateItemName = Console.ReadLine();
                if (updateItemName == "")
                {
                    Console.WriteLine(" Name can't Be null");
                    goto UpdateItemAgain;
                }
                else
                {
                    if (ItemAlreadyExistOrNot(updateItemName))
                    {
                        string oldtonewname;
                        double? newPrice;
                        double? NewQuantity;
                        try
                        {
                            Console.WriteLine("Enter the New Name  to Update");
                            oldtonewname = Console.ReadLine();
                            Console.WriteLine("Enter the New Price");
                            newPrice = Convert.ToDouble(Console.ReadLine());
                            Console.WriteLine("Enter the New Quantity");
                            NewQuantity = Convert.ToDouble(Console.ReadLine());
                            if (oldtonewname == "" || newPrice == null || NewQuantity == null)
                            {
                                Console.WriteLine("Please Entre Again");
                                goto UpdateItemAgain;

                            }
                            else
                            {
                                string sql = "update OrderItem set ItemName='" + oldtonewname + "',ItemPrice=" + newPrice + ",ItemQuantity=" + NewQuantity + " where ItemName='" + updateItemName + "'";
                                sqlDataAdapter = new SqlDataAdapter(sql, _connection);
                                sqlDataAdapter.Fill(getDataItem);
                                Console.WriteLine("Item Update Successfully");
                                Console.WriteLine();
                                Console.WriteLine("Do you Want TO more update Items\n              Press: 1");
                                int chechk = int.Parse(Console.ReadLine());
                                if (chechk == 1)
                                {
                                    goto UpdateItemAgain;
                                }

                            }

                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.ToString() + "\nPlease Enter All Values Correct...");
                            goto UpdateItemAgain;

                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine(ex2.ToString() + "\ntry Next time");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Item Does Not Exist ");
                        Console.WriteLine("Try Again ....");
                        goto UpdateItemAgain;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Try Latter ...");

            }

        }
        private void ShowAllItem()
        {
            try
            {
                string sql = "select * from OrderItem";
                sqlDataAdapter = new SqlDataAdapter(sql, _connection);
                sqlDataAdapter.Fill(getDataItem);
                if (getDataItem.Rows.Count > 0)
                {
                    //print column
                    for (int i = 0; i < getDataItem.Columns.Count; i++)
                    {
                        Console.Write(getDataItem.Columns[i].ColumnName + "| ");

                    }
                    Console.WriteLine();

                    //print data
                    for (int i = 0; i < getDataItem.Rows.Count; i++)
                    {
                        for (int j = 0; j < getDataItem.Columns.Count; j++)
                        {
                            Console.Write(getDataItem.Rows[i][j] + "        ");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Record Found...");
                }
            }
            catch (Exception ex)
            {

            }
            getDataItem = null;

        }
        private bool ItemAlreadyExistOrNot(string ItemName)
        {
            DataTable dataTable = new DataTable();
            string sql = "select * from OrderItem where ItemName='" + ItemName + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, _connection);
            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }
        private void ItemMasterMenu()

        {
            Console.WriteLine();
            
            Console.WriteLine("Add Item      Press :1  ");
            Console.WriteLine("Delete Item   Press :2  ");
            Console.WriteLine("update Item   Press :3  ");
            Console.WriteLine("Show All Item Press :4  ");
            Console.WriteLine("Exit          Press :5  ");
            
            Console.WriteLine();
        }
        public void ItemMasterPortal()
        {

        Portal:
            ItemMasterMenu();
            int switch_on = Convert.ToInt32(Console.ReadLine());
            switch (switch_on)
            {
                case 1:
                    AddItem();
                    goto Portal;
                case 2:
                    DeleteItem();
                    goto Portal;
                case 3:
                    UpdateItem();
                    goto Portal;
                case 4:
                    ShowAllItem();
                    goto Portal;
                case 5:

                    break;


                default:
                    Console.WriteLine("Try Again");
                    goto Portal;
            }

        }
    }
}