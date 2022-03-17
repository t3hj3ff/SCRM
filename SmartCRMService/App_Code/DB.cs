using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using SmartCRMService.App_Code.Communication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SmartCRMService.App_Code
{
    public class DB
    {
        string constr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.2.150.254)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=WF)));User Id=wf;Password=wfdb12098;";
        // string constr = string.Format("Data Source=WF;Persist Security Info=True;User ID={0};Password={1};Connection Lifetime=0; Pooling=false;","wf","wfdb12098");

        public Dashboard GetDashboardData(int user_id, DateTime generate_date)
        {
            Dashboard data = new Dashboard();
            data.Customer = new CustomerSturcure();
            data.CustomerAges = new List<DashboardItemBase>();
            data.CustomerGenders = new List<DashboardItemBase>();
            data.SMS = new RemainingSMS();
            data.VisitTurnovers = new List<VisitTurnover>();
            data.VizitorRanges = new List<VizitorRange>();

            DataTable dt = new DataTable();
            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.get_dashboard"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_crm_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_crm_user_id"].Value = user_id;

                    cmd.Parameters.Add("p_generate_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_generate_date"].Value = generate_date;

                    cmd.Parameters.Add("f_customer_ages", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_customer_gender", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_visits_turnovers ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_customer_structure ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_customer_remaining_sms_point ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("f_vizitor_range_stat ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.CustomerAges.Add(new DashboardItemBase { Count = Convert.ToInt32(rd["cnt"].ToString()), Description = rd["DESCRIP"].ToString() });
                    }
                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.CustomerGenders.Add(new DashboardItemBase { Count = Convert.ToInt32(rd["cnt"].ToString()), Description = rd["GENDER_DESCR"].ToString() });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.VisitTurnovers.Add(new VisitTurnover
                        {
                            Day = rd["GENERATE_DATE"].ToString(),
                            Turnover = (Decimal)rd["TURNOVERS_DAY"],
                            Visit = Convert.ToInt32(rd["VISITS_DAY"].ToString()),
                            AvgAmount = (Decimal)rd["AVG_AMOUNT"],
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.Customer.ActiveClients = Convert.ToInt32(rd["ACTIVE_CLIENTS"].ToString());
                        data.Customer.AverageAmount = (Decimal)rd["AVERAGE_AMOUNT"];
                        data.Customer.InActiveClients = Convert.ToInt32(rd["INACTIVE_CLIENTS"].ToString());
                        data.Customer.NewClients = Convert.ToInt32(rd["NEW_CLIENTS"].ToString());
                        data.Customer.TotalClients = Convert.ToInt32(rd["TOTAL_CLIENTS"].ToString());
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.SMS.PointQuantity = (Decimal)rd["POINT_QUANTITY"];
                        data.SMS.SMSQuantity = Convert.ToInt32(rd["SMS_QUANTITY"].ToString());
                        data.SMS.SMSRemaining = Convert.ToInt32(rd["Sms_Remaining"].ToString());
                        data.SMS.PointRemaining = Convert.ToInt32(rd["Point_Remaining"].ToString());
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.VizitorRanges.Add(new VizitorRange
                        {
                            Description = rd["VISIT_RANGE_DESCRIP"].ToString(),
                            Count = Convert.ToInt32(rd["VISITOR_CNT"].ToString()),
                            Amount = (Decimal)rd["AMOUNT"],
                            VisitorCntPerc = (Decimal)rd["VISITOR_CNT_PERC"],
                            AmountPerc = (Decimal)rd["AMOUNT_PERC"],
                            AverageAmount = (Decimal)rd["AVG_AMOUNT"],
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public Dashboard GetDashboardDataByOrg(int org_id, DateTime generate_date)
        {
            Dashboard data = new Dashboard();
            data.Customer = new CustomerSturcure();
            data.CustomerAges = new List<DashboardItemBase>();
            data.CustomerGenders = new List<DashboardItemBase>();
            data.SMS = new RemainingSMS();
            data.VisitTurnovers = new List<VisitTurnover>();
            data.VizitorRanges = new List<VizitorRange>();

            DataTable dt = new DataTable();
            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.GET_DASHBOARD_BY_ORG"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = org_id;

                    cmd.Parameters.Add("p_generate_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_generate_date"].Value = generate_date;

                    cmd.Parameters.Add("f_customer_ages", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_customer_gender", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_visits_turnovers ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_customer_structure ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("f_customer_remaining_sms_point ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("f_vizitor_range_stat ", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.CustomerAges.Add(new DashboardItemBase { Count = Convert.ToInt32(rd["cnt"].ToString()), Description = rd["DESCRIP"].ToString() });
                    }
                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.CustomerGenders.Add(new DashboardItemBase { Count = Convert.ToInt32(rd["cnt"].ToString()), Description = rd["GENDER_DESCR"].ToString() });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.VisitTurnovers.Add(new VisitTurnover { Day = rd["GENERATE_DATE"].ToString(), Turnover = (Decimal)rd["TURNOVERS_DAY"], Visit = Convert.ToInt32(rd["VISITS_DAY"].ToString()) });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.Customer.ActiveClients = Convert.ToInt32(rd["ACTIVE_CLIENTS"].ToString());
                        data.Customer.AverageAmount = (Decimal)rd["AVERAGE_AMOUNT"];
                        data.Customer.InActiveClients = Convert.ToInt32(rd["INACTIVE_CLIENTS"].ToString());
                        data.Customer.NewClients = Convert.ToInt32(rd["NEW_CLIENTS"].ToString());
                        data.Customer.TotalClients = Convert.ToInt32(rd["TOTAL_CLIENTS"].ToString());
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.SMS.PointQuantity = (Decimal)rd["POINT_QUANTITY"];
                        data.SMS.SMSQuantity = Convert.ToInt32(rd["SMS_QUANTITY"].ToString());
                        data.SMS.SMSRemaining = Convert.ToInt32(rd["Sms_Remaining"].ToString());
                        data.SMS.PointRemaining = Convert.ToInt32(rd["Point_Remaining"].ToString());
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.VizitorRanges.Add(new VizitorRange
                        {
                            Description = rd["VISIT_RANGE_DESCRIP"].ToString(),
                            Count = Convert.ToInt32(rd["VISITOR_CNT"].ToString()),
                            Amount = (Decimal)rd["AMOUNT"],
                            VisitorCntPerc = (Decimal)rd["VISITOR_CNT_PERC"],
                            AmountPerc = (Decimal)rd["AMOUNT_PERC"],
                            AverageAmount = (Decimal)rd["AVG_AMOUNT"],
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public LoginResult Login(LoginModel m)
        {
            LoginResult res = new LoginResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.Authorization"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_name", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_name"].Value = m.UserName;

                    cmd.Parameters.Add("p_pass", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_pass"].Value = m.Password;

                    cmd.Parameters.Add("p_lang", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_lang"].Value = "KA";

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_org_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_fullname", OracleDbType.NVarchar2, 128).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_role_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_org_name", OracleDbType.NVarchar2, 128).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("p_error_code", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_error_desc", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["p_error_code"].Value.ToString());

                    res.UserID = !((INullable)cmd.Parameters["p_user_id"].Value).IsNull ? Convert.ToInt32(cmd.Parameters["p_user_id"].Value.ToString()) : 0;
                    res.RoleID = Convert.ToInt32(cmd.Parameters["p_role_id"].Value.ToString());
                    res.FullName = !((INullable)cmd.Parameters["p_fullname"].Value).IsNull ? cmd.Parameters["p_fullname"].Value.ToString() : "";
                    res.OrgName = !((INullable)cmd.Parameters["p_org_name"].Value).IsNull ? cmd.Parameters["p_org_name"].Value.ToString() : "";
                    res.OrgID = !((INullable)cmd.Parameters["p_org_id"].Value).IsNull ? cmd.Parameters["p_org_id"].Value.ToString() : "";
                    res.ResultMsg = cmd.Parameters["p_error_desc"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public DataTable GetSQLData(string sql, string user, string password)
        {
            try
            {
                // Connect
                using (var con = Connect(constr))
                {
                    con.ConnectionString = constr;

                    OracleCommand cmd = con.CreateCommand();

                    OracleDataAdapter oraDataAdapter = new OracleDataAdapter();
                    DataTable DTable = new DataTable();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;

                    OracleDataReader dr = cmd.ExecuteReader();
                    DTable.Load(dr);

                    dr.Close();
                    con.Close();

                    dr.Dispose();
                    cmd.Dispose();
                    con.Dispose();

                    return DTable;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
            }

        }

        public List<Organization> GetOrganizations()
        {
            var orgs = new List<Organization>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.Get_Organizations"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_organizations", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        orgs.Add(new Organization
                        {
                            ID = Convert.ToInt32(rd["org_id"].ToString()),
                            Name = rd["org_name"].ToString(),
                            Title_ka = rd["name_ka"].ToString(),
                            Title_en = rd["name_en"].ToString()
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return orgs;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return orgs;


        }

        public OracleConnection Connect(string connectStr)
        {
            OracleConnection con = new OracleConnection(connectStr);

            con.Open();
            return con;
        }

        public ReportData GetReportData(int user_id, DateTime generate_date)
        {
            ReportData data = new ReportData();
            data.Amounts = new List<ReportAmount>();
            data.AvgAmounts = new List<ReportAvgAmount>();
            data.Transactions = new List<ReportTransaction>();
            data.CustomerStructure = new ReportCustomerStructure();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.crm_rep_get_data"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    //cmd.Parameters.Add("p_crm_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    //cmd.Parameters["p_crm_user_id"].Value = user_id;

                    cmd.Parameters.Add("p_group_By_Date", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Date"].Value = generate_date;

                    cmd.Parameters.Add("p_group_By_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Gender"].Value = generate_date;

                    cmd.Parameters.Add("p_Group_By_Age", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Age"].Value = generate_date;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = generate_date;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = generate_date;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = generate_date;


                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = generate_date;


                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = generate_date;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = generate_date;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = generate_date;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = generate_date;

                    cmd.Parameters.Add("p_Amounts_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_Tranzactions_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_avg_amount_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_Customer_Structure_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_Gender_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("p_Age_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Amounts.Add(new ReportAmount
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            GenderID = (int?)rd["Gender_ID"],
                            AgeRangeID = (int?)rd["age_range_id"],
                            Amount = (decimal)rd["Amount"]
                        });
                    }
                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.Transactions.Add(new ReportTransaction
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            GenderID = (int?)rd["Gender_ID"],
                            AgeRangeID = (int?)rd["age_range_id"],
                            TranCount = Convert.ToInt32(rd["tran_cnt"].ToString())
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.AvgAmounts.Add(new ReportAvgAmount
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            GenderID = (int?)rd["Gender_ID"],
                            AgeRangeID = (int?)rd["age_range_id"],
                            AvgAmount = (decimal)rd["AVG_Amount"]
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.CustomerStructure.CustomerCountDay = Convert.ToInt32(rd["CUST_CNT_DAY"].ToString());
                        data.CustomerStructure.NewCustomerCountDay = Convert.ToInt32(rd["NEW_CUST_CNT_DAY"].ToString());
                        data.CustomerStructure.ReactivateCustomerCountDay = Convert.ToInt32(rd["REACTIVATE_CUST_CNT_DAY"].ToString());
                        data.CustomerStructure.ActiveCustomerCount = Convert.ToInt32(rd["ACTIVE_CUST_CNT"].ToString());
                        data.CustomerStructure.PassiveCustomerCount = Convert.ToInt32(rd["PASSIVE_CUST_CNT"].ToString());
                    }

                    //rd.NextResult();
                    //while (rd.Read())
                    //{
                    //    data.SMS.PointQuantity = (Decimal)rd["POINT_QUANTITY"];
                    //    data.SMS.SMSQuantity = Convert.ToInt32(rd["SMS_QUANTITY"].ToString());
                    //    data.SMS.SMSRemaining = Convert.ToInt32(rd["Sms_Remaining"].ToString());
                    //    data.SMS.PointRemaining = Convert.ToInt32(rd["Point_Remaining"].ToString());
                    //}

                    //rd.NextResult();
                    //while (rd.Read())
                    //{
                    //    data.VizitorRanges.Add(new VizitorRange
                    //    {
                    //        Description = rd["VISIT_RANGE_DESCRIP"].ToString(),
                    //        Count = Convert.ToInt32(rd["VISITOR_CNT"].ToString()),
                    //        Amount = (Decimal)rd["AMOUNT"],
                    //        VisitorCntPerc = (Decimal)rd["VISITOR_CNT_PERC"],
                    //        AmountPerc = (Decimal)rd["AMOUNT_PERC"],
                    //        AverageAmount = (Decimal)rd["AVG_AMOUNT"],
                    //    });
                    //}

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportAmount> GetReportAmountData(ReportModelBase m)
        {
            List<ReportAmount> data = new List<ReportAmount>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Amount_cursor"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_group_By_Date", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Date"].Value = m.GroupByDate;

                    cmd.Parameters.Add("p_group_By_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Gender"].Value = m.GroupByGender;

                    cmd.Parameters.Add("p_Group_By_Age", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Age"].Value = m.GroupByAge;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Amounts_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new ReportAmount
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            GenderID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Gender_ID"].ToString()),
                            AgeRangeID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["age_range_id"].ToString()),
                            Amount = (decimal)rd["Amount"]
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportTransaction> GetReportTransactionData(ReportModelBase m)
        {
            List<ReportTransaction> data = new List<ReportTransaction>();
            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Tranz_Cursor"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_group_By_Date", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Date"].Value = m.GroupByDate;

                    cmd.Parameters.Add("p_group_By_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Gender"].Value = m.GroupByGender;

                    cmd.Parameters.Add("p_Group_By_Age", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Age"].Value = m.GroupByAge;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Tranzactions_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();


                    while (rd.Read())
                    {
                        data.Add(new ReportTransaction
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            GenderID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Gender_ID"].ToString()),
                            AgeRangeID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["age_range_id"].ToString()),
                            TranCount = Convert.ToInt32(rd["tran_cnt"].ToString())
                        });
                    }


                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportAvgAmount> GetReportAverageAmountData(ReportModelBase m)
        {
            var data = new List<ReportAvgAmount>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Avg_amount_cursor"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_group_By_Date", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Date"].Value = m.GroupByDate;

                    cmd.Parameters.Add("p_group_By_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Gender"].Value = m.GroupByGender;

                    cmd.Parameters.Add("p_Group_By_Age", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Age"].Value = m.GroupByAge;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_avg_amount_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();


                    while (rd.Read())
                    {
                        data.Add(new ReportAvgAmount
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            GenderID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Gender_ID"].ToString()),
                            AgeRangeID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["age_range_id"].ToString()),
                            AvgAmount = (decimal)rd["AVG_Amount"]
                        });
                    }



                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportFilial> GetReportFilials(int? UserID, int? OrgID)
        {
            var data = new List<ReportFilial>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Filials"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_ID"].Value = UserID;

                    cmd.Parameters.Add("p_org_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_org_id"].Value = OrgID;

                    cmd.Parameters.Add("p_Filials", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new ReportFilial
                        {
                            ObjectID = Convert.ToInt32(rd["object_id"].ToString()),
                            MerchName = rd["Merch_Name"].ToString(),
                            CityName = rd["city_name"].ToString(),
                            RaioniName = rd["raioni_name"].ToString(),
                            StreetName = rd["street_name"].ToString(),
                            Address = rd["Address"].ToString()
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportStructure> GetReportStructureData(ReportModelBase m)
        {
            var data = new List<ReportStructure>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Structure_cursor"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;


                    cmd.Parameters.Add("p_group_By_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Gender"].Value = m.GroupByGender;

                    cmd.Parameters.Add("p_Group_By_Age", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Age"].Value = m.GroupByAge;

                    cmd.Parameters.Add("p_Group_By_Filial", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Filial"].Value = m.GroupByFilial;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Customer_Structure_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();


                    while (rd.Read())
                    {
                        data.Add(new ReportStructure
                        {
                            Address = rd["OBJECT_address"] != DBNull.Value ? rd["OBJECT_address"].ToString() : "",
                            AgeRange = rd["AGE_RANGE"] == DBNull.Value ? "" : rd["AGE_RANGE"].ToString(),
                            Gender = rd["Gender"] == DBNull.Value ? "" : rd["Gender"].ToString(),
                            ActiveCustomerCount = Convert.ToInt32(rd["ACTIVE_CUSTOMER_CNT"].ToString()),
                            NewCustomerCount = Convert.ToInt32(rd["NEW_CUSTOMER_CNT"].ToString()),
                            PassiveCustomerCount = Convert.ToInt32(rd["PASSIVE_CUSTOMER_CNT"].ToString()),
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportGender> GetReportGenderData(ReportModelBase m)
        {
            List<ReportGender> data = new List<ReportGender>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Gender_cursor"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    //cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    //cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Customer_Gender_cursorS", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new ReportGender
                        {
                            Description = rd["descrip"].ToString(),
                            Count = rd["CNT"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["CNT"].ToString())
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportGender> GetReportAgesData(ReportModelBase m)
        {
            List<ReportGender> data = new List<ReportGender>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Age_Range_cursor"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    //cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    //cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Customer_Age_Range_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new ReportGender
                        {
                            Description = rd["descrip"].ToString(),
                            Count = rd["CNT"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["CNT"].ToString())
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportTransItem> GetReportTransData(ReportModelBase m)
        {
            List<ReportTransItem> data = new List<ReportTransItem>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_Get_Report_Trans_data"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_group_By_Date", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Date"].Value = m.GroupByDate;

                    cmd.Parameters.Add("p_group_By_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_group_By_Gender"].Value = m.GroupByGender;

                    cmd.Parameters.Add("p_Group_By_Age", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Group_By_Age"].Value = m.GroupByAge;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Amounts_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new ReportTransItem
                        {
                            DatePeriod = rd["Date_Period"].ToString(),
                            Amount = (decimal)rd["Amount"],
                            TranCount = Convert.ToInt32(rd["tran_cnt"].ToString()),
                            AvgAmount = (decimal)rd["AVG_Amount"]
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public List<ReportTransExportItem> GetReportExportData(ReportModelBase m)
        {
            List<ReportTransExportItem> data = new List<ReportTransExportItem>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_REP_EXPORT_DATA"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_Filter_DATE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_Filter_DATE_ID"].Value = m.FilterDateID;

                    cmd.Parameters.Add("p_Filter_Start_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Start_date"].Value = m.FilterStartDate;

                    cmd.Parameters.Add("p_Filter_End_date", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_End_date"].Value = m.FilterEndDate;

                    cmd.Parameters.Add("p_Filter_Gender", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Gender"].Value = m.FilterGender;

                    cmd.Parameters.Add("p_Filter_age_range_id", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_age_range_id"].Value = m.FilterAgeRangeID;

                    cmd.Parameters.Add("p_Filter_merch_name", OracleDbType.NVarchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_merch_name"].Value = m.FilterMerchname;

                    cmd.Parameters.Add("p_Filter_Object_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Object_ID"].Value = m.FilterObjectID;

                    cmd.Parameters.Add("p_Filter_Org_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Filter_Org_ID"].Value = m.FilterOrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_Tranzaction_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new ReportTransExportItem
                        {
                            AuthDateTime = rd["auth_date_time"].ToString(),
                            //GenderID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["Gender_ID"].ToString()),
                            //AgeRangeID = rd["Gender_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rd["age_range_id"].ToString()),
                            Card = rd["card"].ToString(),
                            ClientName = rd["f_names_g"].ToString(),
                            Gender = rd["gender"].ToString(),
                            AgeRange = rd["age_range"].ToString(),
                            Amount = (Decimal)rd["Amount"],
                            TerminalId = rd["terminal_id"].ToString(),
                            OrgCode = rd["org_iden_num"].ToString(),
                            Address = rd["Address"].ToString()
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;

        }

        public RFMStats GetRFMStats(RFMStatModel m)
        {
            RFMStats data = new RFMStats();
            data.S_ID_List = new List<RFMSIDItem>();
            data.R = new List<RFMRFItem>();
            data.F = new List<RFMRFItem>();
            data.Cells = new List<RFMCell>();
            data.Segments = new List<RFMSegment>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.RFM_GET_SID_STATS"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;


                    cmd.Parameters.Add("P_SID_STATS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_SID_R_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_SID_F_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_SID_CELLS_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_SEGMENT_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.S_ID_List.Add(new RFMSIDItem
                        {
                            Description = rd["descrip"].ToString(),
                            S_id = rd["sid"].ToString()
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.R.Add(new RFMRFItem
                        {
                            ID = rd["sCORE"].ToString(),
                            Description = rd["R_VALUES"].ToString()
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.F.Add(new RFMRFItem
                        {
                            ID = rd["sCORE"].ToString(),
                            Description = rd["F_VALUES"].ToString()
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.Cells.Add(new RFMCell
                        {
                            SID = rd["SID"].ToString(),
                            R = rd["R"].ToString(),
                            F = rd["F"].ToString(),
                            CARD_CNT = Convert.ToInt32(rd["CARD_CNT"].ToString()),
                            Description = rd["descrip"].ToString()
                        });
                    }

                    rd.NextResult();
                    while (rd.Read())
                    {
                        data.Segments.Add(new RFMSegment
                        {
                            ID = Convert.ToInt32(rd["ID"].ToString()),
                            Name = rd["NAME"].ToString(),
                            CustomerCount = Convert.ToInt32(rd["CUSTOMER_CNT"].ToString())
                        });
                    }



                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        public RFMAddSegmentResult RFMAddSegment(RFMAddSegmentModel m)
        {
            RFMAddSegmentResult res = new RFMAddSegmentResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.RFM_ADD_SEGMENT"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_NAME", OracleDbType.NVarchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_NAME"].Value = m.Name;

                    cmd.Parameters.Add("P_COORDINATES", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_COORDINATES"].Value = m.Coordinates;

                    cmd.Parameters.Add("P_AGE_RANGE_ID", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_AGE_RANGE_ID"].Value = m.AgeRangeID;

                    cmd.Parameters.Add("P_GENDER_ID", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_GENDER_ID"].Value = m.GenderID;

                    cmd.Parameters.Add("P_FILIAL", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_FILIAL"].Value = m.Filial;

                    cmd.Parameters.Add("p_SEGMENT_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();

                    res.ID = Convert.ToInt32(cmd.Parameters["p_SEGMENT_ID"].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public RFMNewSegmentScoreResult RFMGetNewSegmentScore(RFMAddSegmentModel m)
        {
            RFMNewSegmentScoreResult res = new RFMNewSegmentScoreResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.RFM_GET_SEGMENT_SCORE"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_COORDINATES", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_COORDINATES"].Value = m.Coordinates;

                    cmd.Parameters.Add("P_AGE_RANGE_ID", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_AGE_RANGE_ID"].Value = m.AgeRangeID;

                    cmd.Parameters.Add("P_GENDER_ID", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_GENDER_ID"].Value = m.GenderID;

                    cmd.Parameters.Add("P_FILIAL", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_FILIAL"].Value = m.Filial;

                    cmd.Parameters.Add("P_SEGMENT_CNT", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    con.Close();

                    res.Count = Convert.ToInt32(cmd.Parameters["P_SEGMENT_CNT"].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public CampaignsAddResult CampaignsAdd(CampaignAddModel m)
        {
            CampaignsAddResult res = new CampaignsAddResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_ADD_CAMPAIGN"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_SEGMENT_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEGMENT_ID"].Value = m.SegmentID;

                    cmd.Parameters.Add("P_SEGMENT_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEGMENT_CNT"].Value = m.SegmentCount;

                    cmd.Parameters.Add("P_ACTION_PERCENT", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_PERCENT"].Value = m.ActionPercent;

                    cmd.Parameters.Add("P_ACTION_CASH_VAUCHER", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_CASH_VAUCHER"].Value = m.ActionCashVaucher;

                    cmd.Parameters.Add("P_ACTION_CASH_VAUCHER_PERC", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_CASH_VAUCHER_PERC"].Value = m.ActionCashVaucherPercent;

                    cmd.Parameters.Add("P_ACTION_CASH_VAUCHER_START", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_CASH_VAUCHER_START"].Value = m.ActionCashVaucherStart;

                    cmd.Parameters.Add("P_ACTION_CASH_VAUSHER_END", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_CASH_VAUSHER_END"].Value = m.ActionCashVaucherEnd;

                    cmd.Parameters.Add("P_ACTION_BONUS_FIX", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_BONUS_FIX"].Value = m.ActionBonusFix;

                    cmd.Parameters.Add("P_ACTION_BONUS_PERC", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_BONUS_PERC"].Value = m.ActionBonusPercent;

                    cmd.Parameters.Add("P_ACTION_PRODUCT_NAME", OracleDbType.NVarchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_PRODUCT_NAME"].Value = m.ActionProductName;

                    cmd.Parameters.Add("P_ACTION_PRODUCT_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_PRODUCT_CNT"].Value = m.ActionProductCount;

                    cmd.Parameters.Add("P_PRESENT_CASE_VISIT_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_VISIT_CNT"].Value = m.PersentCaseVisitCount;

                    cmd.Parameters.Add("P_PRESENT_CASE_VISIT_AMOUNT", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_VISIT_AMOUNT"].Value = m.PersentCaseVisitAmount;

                    //cmd.Parameters.Add("P_PRESENT_CASE_VISIT_RECIV_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    //cmd.Parameters["P_PRESENT_CASE_VISIT_RECIV_CNT"].Value = m.PersentCaseVisitReceiveCount;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_AMOUNT", OracleDbType.Decimal).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_AMOUNT"].Value = m.PersentCaseSpentAmount;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_CNT"].Value = m.PersentCaseSpentCount;

                    //cmd.Parameters.Add("P_PRESENT_CASE_SPENT_RECIV_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    //cmd.Parameters["P_PRESENT_CASE_SPENT_RECIV_CNT"].Value = m.PersentCaseSpentReceiveCount;


                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_1", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_1"].Value = m.PersentCaseSpentWDay1;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_2", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_2"].Value = m.PersentCaseSpentWDay2;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_3", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_3"].Value = m.PersentCaseSpentWDay3;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_4", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_4"].Value = m.PersentCaseSpentWDay4;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_5", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_5"].Value = m.PersentCaseSpentWDay5;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_6", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_6"].Value = m.PersentCaseSpentWDay6;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_7", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_7"].Value = m.PersentCaseSpentWDay7;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_s_t", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_s_t"].Value = m.PersentCaseSpentWDayStartTime;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_W_DAY_e_t", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_W_DAY_e_t"].Value = m.PersentCaseSpentWDayEndTime;

                    cmd.Parameters.Add("P_PRESENT_CASE_SPENT_UNCONDITI", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_SPENT_UNCONDITI"].Value = m.PersentCaseSpentUncondition;

                    cmd.Parameters.Add("P_PRESENT_CASE_RECIVE_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_CASE_RECIVE_CNT"].Value = m.PersentCaseReceiveCount;


                    cmd.Parameters.Add("P_ACTION_START_DATE", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_START_DATE"].Value = m.ActionStartDate;

                    cmd.Parameters.Add("P_ACTION_END_DATE", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ACTION_END_DATE"].Value = m.ActionEndDate;

                    cmd.Parameters.Add("P_PRESENT_LOCATION", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_PRESENT_LOCATION"].Value = m.PresentLocation;

                    //cmd.Parameters.Add("P_PREZENT_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    //cmd.Parameters["P_PREZENT_TEXT"].Value = m.PresentText;

                    cmd.Parameters.Add("P_CAMP_NAME", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CAMP_NAME"].Value = m.ChampName;

                    cmd.Parameters.Add("P_CAMP_DESCRIP", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CAMP_DESCRIP"].Value = m.ChampDescription;

                    cmd.Parameters.Add("p_CAMP_LIST_ID_FOR_DEL", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_CAMP_LIST_ID_FOR_DEL"].Value = m.CampID;

                    cmd.Parameters.Add("p_SEND_SMS_DATE", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_SEND_SMS_DATE"].Value = m.SendMSGDate;

                    cmd.Parameters.Add("p_MSG_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_MSG_TEXT"].Value = m.MsgText;

                    cmd.Parameters.Add("p_REMIND_MSG_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_REMIND_MSG_TEXT"].Value = m.RemindMsgText;

                    cmd.Parameters.Add("p_PERFROM_MSG_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_PERFROM_MSG_TEXT"].Value = m.PerformMsgText;

                    cmd.Parameters.Add("p_HAS_BLACK_LIST", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_HAS_BLACK_LIST"].Value = m.HasBlackList;

                    cmd.Parameters.Add("p_SEND_TEST_MSG", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_SEND_TEST_MSG"].Value = m.SendTestMSG;

                    cmd.Parameters.Add("p_ADD_GROUP_IN_ACTION", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_ADD_GROUP_IN_ACTION"].Value = m.AddGroupInAction;

                    cmd.Parameters.Add("P_SEGMENT_COMPARE_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEGMENT_COMPARE_CNT"].Value = m.SegmentCompareCount;

                    cmd.Parameters.Add("p_CAMP_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                    res.ID = Convert.ToInt32(cmd.Parameters["p_CAMP_LIST_ID"].Value.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public List<CampaignItem> GetCampaigns(GetCampaignsModel m)
        {
            var data = new List<CampaignItem>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_GET_CAMPAIGNS"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_ID"].Value = m.UserID;

                    cmd.Parameters.Add("p_org_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_org_id"].Value = m.OrgID;

                    cmd.Parameters.Add("P_STATUS", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_STATUS"].Value = m.Status;

                    cmd.Parameters.Add("P_CRM_CAMP_LIST_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new CampaignItem
                        {
                            ID = Convert.ToInt32(rd["ID"].ToString()),
                            Name = rd["NAME"].ToString(),
                            Period = rd["PERIOD"].ToString(),
                            Status = rd["STATUS"].ToString(),
                            StatusName = rd["STATUS_NAME"].ToString()
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        public CampaignDetails GetCampaignDetails(int CampID)
        {
            var data = new CampaignDetails();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_GET_CAMPAIGN_DET"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_CAMP_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_CAMP_LIST_ID"].Value = CampID;

                    cmd.Parameters.Add("p_CAMPAIGN_DET_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.ActionBonusFix = rd["ACTION_BONUS_FIX"] != DBNull.Value ? (decimal?)rd["ACTION_BONUS_FIX"] : (decimal?)null;
                        data.ActionBonusPercent = rd["ACTION_BONUS_PERC"] != DBNull.Value ? (decimal?)rd["ACTION_BONUS_PERC"] : (decimal?)null;
                        data.ActionCashVaucher = rd["ACTION_CASH_VAUCHER"] != DBNull.Value ? (decimal?)rd["ACTION_CASH_VAUCHER"] : (decimal?)null;

                        data.ActionCashVaucherEnd = rd["ACTION_CASH_VAUSHER_END"] != DBNull.Value ? (DateTime?)rd["ACTION_CASH_VAUSHER_END"] : (DateTime?)null;
                        data.ActionCashVaucherPercent = rd["ACTION_CASH_VAUCHER_PERC"] != DBNull.Value ? (decimal?)rd["ACTION_CASH_VAUCHER_PERC"] : (decimal?)null;

                        data.ActionCashVaucherStart = rd["ACTION_CASH_VAUCHER_START"] != DBNull.Value ? (DateTime?)rd["ACTION_CASH_VAUCHER_START"] : (DateTime?)null;

                        data.ActionEndDate = rd["Action_END_Date"] != DBNull.Value ? (DateTime?)rd["Action_END_Date"] : (DateTime?)null;
                        data.ActionStartDate = rd["Action_Start_Date"] != DBNull.Value ? (DateTime?)rd["Action_Start_Date"] : (DateTime?)null;

                        data.ActionPercent = rd["ACTION_PERCENT"] != DBNull.Value ? (decimal?)rd["ACTION_PERCENT"] : (decimal?)null;
                        data.ActionProductCount = rd["ACTION_PRODUCT_CNT"] != DBNull.Value ? Convert.ToInt32(rd["ACTION_PRODUCT_CNT"].ToString()) : (int?)null;
                        data.ActionProductName = rd["ACTION_PRODUCT_NAME"] != DBNull.Value ? rd["ACTION_PRODUCT_NAME"].ToString() : null;
                        data.CampID = rd["crm_camp_list_id"] != DBNull.Value ? Convert.ToInt32(rd["crm_camp_list_id"].ToString()) : (int?)null;

                        data.ChampDescription = rd["CAMP_DESCRIP"] != DBNull.Value ? rd["CAMP_DESCRIP"].ToString() : null;

                        data.ChampName = rd["CAMP_NAME"] != DBNull.Value ? rd["CAMP_NAME"].ToString() : null;

                        data.OrgID = rd["ORG_ID"] != DBNull.Value ? Convert.ToInt32(rd["ORG_ID"].ToString()) : (int?)null;

                        data.PersentCaseSpentAmount = rd["PRESENT_CASE_SPENT_AMOUNT"] != DBNull.Value ? (decimal?)rd["PRESENT_CASE_SPENT_AMOUNT"] : (decimal?)null;


                        data.PersentCaseSpentCount = rd["PRESENT_CASE_SPENT_CNT"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_CNT"].ToString()) : (int?)null;

                        //  data.PersentCaseSpentReceiveCount = rd["PRESENT_CASE_SPENT_RECIV_CNT"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_RECIV_CNT"].ToString()) : (int?)null;
                        data.PersentCaseReceiveCount = rd["present_case_recive_cnt"] != DBNull.Value ? Convert.ToInt32(rd["present_case_recive_cnt"].ToString()) : (int?)null;

                        data.PersentCaseSpentUncondition = rd["PRESENT_CASE_SPENT_UNCONDITION"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_UNCONDITION"].ToString()) : (int?)null;

                        data.PersentCaseSpentWDay1 = rd["PRESENT_CASE_SPENT_W_DAY_1"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_1"].ToString()) : (int?)null;
                        data.PersentCaseSpentWDay2 = rd["PRESENT_CASE_SPENT_W_DAY_2"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_2"].ToString()) : (int?)null;
                        data.PersentCaseSpentWDay3 = rd["PRESENT_CASE_SPENT_W_DAY_3"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_3"].ToString()) : (int?)null;
                        data.PersentCaseSpentWDay4 = rd["PRESENT_CASE_SPENT_W_DAY_4"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_4"].ToString()) : (int?)null;
                        data.PersentCaseSpentWDay5 = rd["PRESENT_CASE_SPENT_W_DAY_5"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_5"].ToString()) : (int?)null;
                        data.PersentCaseSpentWDay6 = rd["PRESENT_CASE_SPENT_W_DAY_6"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_6"].ToString()) : (int?)null;
                        data.PersentCaseSpentWDay7 = rd["PRESENT_CASE_SPENT_W_DAY_7"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_SPENT_W_DAY_7"].ToString()) : (int?)null;

                        data.PersentCaseSpentWDayEndTime = rd["PRESENT_CASE_SPENT_W_DAY_e_t"] != DBNull.Value ? rd["PRESENT_CASE_SPENT_W_DAY_e_t"].ToString() : null;

                        data.PersentCaseSpentWDayStartTime = rd["PRESENT_CASE_SPENT_W_DAY_s_T"] != DBNull.Value ? rd["PRESENT_CASE_SPENT_W_DAY_s_T"].ToString() : null;

                        data.PersentCaseVisitAmount = rd["PRESENT_CASE_VISIT_AMOUNT"] != DBNull.Value ? (decimal?)rd["PRESENT_CASE_VISIT_AMOUNT"] : (decimal?)null;

                        data.PersentCaseVisitCount = rd["Present_Case_Visit_Cnt"] != DBNull.Value ? Convert.ToInt32(rd["Present_Case_Visit_Cnt"].ToString()) : (int?)null;

                        //  data.PersentCaseVisitReceiveCount = rd["PRESENT_CASE_VISIT_RECIV_CNT"] != DBNull.Value ? Convert.ToInt32(rd["PRESENT_CASE_VISIT_RECIV_CNT"].ToString()) : (int?)null;

                        data.PresentLocation = rd["PRESENT_LOCATION"] != DBNull.Value ? rd["PRESENT_LOCATION"].ToString() : null;

                        data.MsgText = rd["MSG_TEXT"] != DBNull.Value ? rd["MSG_TEXT"].ToString() : null;

                        data.SegmentCount = rd["SEGMENT_CNT"] != DBNull.Value ? Convert.ToInt32(rd["SEGMENT_CNT"].ToString()) : 0;

                        data.SegmentID = rd["SEGMENT_ID"] != DBNull.Value ? Convert.ToInt32(rd["SEGMENT_ID"].ToString()) : 0;

                        data.SendMSGDate = rd["SEND_MSG_DATE"] != DBNull.Value ? (DateTime?)rd["SEND_MSG_DATE"] : (DateTime?)null;

                        data.RemindMsgText = rd["REMIND_MSG_TEXT"] != DBNull.Value ? rd["REMIND_MSG_TEXT"].ToString() : null;
                        data.PerformMsgText = rd["PERFROM_MSG_TEXT"] != DBNull.Value ? rd["PERFROM_MSG_TEXT"].ToString() : null;
                        data.HasBlackList = rd["HAS_BLACK_LIST"] != DBNull.Value ? rd["HAS_BLACK_LIST"].ToString() : null;
                        data.SendTestMSG = rd["SEND_TEST_MSG"] != DBNull.Value && rd["SEND_TEST_MSG"].ToString() == "1";
                        data.AddGroupInAction = rd["ADD_GROUP_IN_ACTION"] != DBNull.Value && rd["ADD_GROUP_IN_ACTION"].ToString() == "1";

                        data.SegmentCompareCount = rd["SEGMENT_COMPARE_CNT"] != DBNull.Value ? Convert.ToInt32(rd["SEGMENT_COMPARE_CNT"].ToString()) : (int?)null;

                        data.Status = rd["Status"] != DBNull.Value ? rd["Status"].ToString() : "";
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        public List<RFMSegment> GetSegments(RFMStatModel m)
        {
            var data = new List<RFMSegment>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_GET_SEGMENT"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_SEGMENT_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new RFMSegment
                        {
                            ID = Convert.ToInt32(rd["ID"].ToString()),
                            Name = rd["NAME"].ToString(),
                            CustomerCount = Convert.ToInt32(rd["CUSTOMER_CNT"].ToString())
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }


        public BlackListConfirmResult BlackListAdd(BlackListModel m)
        {
            BlackListConfirmResult res = new BlackListConfirmResult();


            if (m.Phones == null)
            {
                m.Phones = new List<string>();
            }


            using (var con = Connect(constr))
            {
                try
                {
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = @"INSERT INTO CRM_BLACK_LIST(PHONE,
                                   BULK_ID,
                                   ORG_ID,
                                   USER_ID)
                        VALUES(:p_phone, :p_bulk_id, :p_org_id, :p_user_id)";
                        command.CommandType = CommandType.Text;
                        command.BindByName = true;
                        // In order to use ArrayBinding, the ArrayBindCount property
                        // of OracleCommand object must be set to the number of records to be inserted
                        command.ArrayBindCount = m.Phones.Count;

                        command.Parameters.Add(":p_phone", OracleDbType.Varchar2, m.Phones.ToArray(), ParameterDirection.Input);
                        command.Parameters.Add(":p_bulk_id", OracleDbType.Varchar2, m.Phones.Select(c => m.BulkID).ToArray(), ParameterDirection.Input);
                        command.Parameters.Add(":p_org_id", OracleDbType.Varchar2, m.Phones.Select(c => m.OrgID).ToArray(), ParameterDirection.Input);
                        command.Parameters.Add(":p_user_id", OracleDbType.Varchar2, m.Phones.Select(c => m.UserID).ToArray(), ParameterDirection.Input);
                        int result = command.ExecuteNonQuery();
                        //if (result == m.Phones.Count)
                        //    returnValue = true;
                    }


                    //foreach (var phone in m.Phones)
                    //{
                    //    var cmd = new OracleCommand
                    //    {
                    //        CommandType = CommandType.StoredProcedure,
                    //        CommandText = "wf.crm_pack.CRM_ADD_BLACK_LIST"
                    //    };

                    //    cmd.Connection = con;

                    //    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    //    cmd.Parameters["p_user_id"].Value = m.UserID;

                    //    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    //    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    //    cmd.Parameters.Add("p_phone", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    //    cmd.Parameters["p_phone"].Value = phone;

                    //    cmd.Parameters.Add("p_bulk_id", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    //    cmd.Parameters["p_bulk_id"].Value = m.BulkID;

                    //    cmd.ExecuteNonQuery();

                    //}



                    res.Result = 200;
                    res.ResultMsg = "შავი სია წარმატებით აიტვირთა";
                }
                catch (Exception ex)
                {
                    res.ResultMsg = ex.Message;
                    res.Result = 500;
                }
                finally
                {
                    con.Close();
                }

            }

            if (res.Result == 200)
            {
                return BlackListConfirm(new BlackListConfirmModel { OrgID = m.OrgID, UserID = m.UserID, BulkID = m.BulkID });
            }
            return res;
        }

        public BlackListResult GetBlackList(ModelBase m)
        {
            var data = new BlackListResult();
            data.Phones = new List<string>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_GET_BLACK_LIST"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("p_BLACK_LIST", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Phones.Add(rd["Phone"].ToString());
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        private BlackListConfirmResult BlackListConfirm(BlackListConfirmModel m)
        {
            BlackListConfirmResult res = new BlackListConfirmResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_BLACK_LIST_CONFIRM"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_BULK_ID", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_BULK_ID"].Value = m.BulkID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_USER_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_USER_ID"].Value = m.UserID;

                    cmd.Parameters.Add("P_NEW_PHONES", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_BAD_NUMBERS", OracleDbType.Int32).Direction = ParameterDirection.Output;


                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = 200;

                    res.NewPhoneCount = cmd.Parameters["P_NEW_PHONES"].Value.ToString();
                    res.BadPhoneCount = cmd.Parameters["P_BAD_NUMBERS"].Value.ToString();
                }
                catch (Exception ex)
                {
                    res.Result = 500;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public ResultBase RFMSegmentDelete(RFMSegmentDeleteModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.RFM_DELETE_SEGMENT"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_segment_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_segment_id"].Value = m.ID;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }

        public ResultBase CampaignDelete(CampaignDeleteModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_DELETE"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_CRM_CAMP_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CRM_CAMP_LIST_ID"].Value = m.ID;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }

        public ResultBase CampaignSendTestMsg(CampaignSendTestMsgModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_SEND_TEST_MSG"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;


                    cmd.Parameters.Add("p_MSG_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_MSG_TEXT"].Value = m.MsgText;

                    cmd.Parameters.Add("p_REMIND_MSG_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_REMIND_MSG_TEXT"].Value = m.RemindMsgText;

                    cmd.Parameters.Add("p_PERFROM_MSG_TEXT", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_PERFROM_MSG_TEXT"].Value = m.PerformMsgText;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }

        public List<Employ> GetEmployes(ModelBase m)
        {
            var data = new List<Employ>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_GET_employes"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_org_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_org_id"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_ID"].Value = m.UserID;

                    cmd.Parameters.Add("p_employes", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new Employ
                        {
                            ID = Convert.ToInt32(rd["ID"].ToString()),
                            Name = rd["NAME"].ToString(),
                            Surname = rd["SURNAME"].ToString(),
                            Card = rd["CARD"].ToString(),
                            Phone = rd["PHONE"].ToString(),
                            Email = rd["Email"].ToString()
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        public EmployResult AddEmployer(EmployModel m)
        {
            EmployResult res = new EmployResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_ADD_employes"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;


                    cmd.Parameters.Add("P_name", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_name"].Value = m.Employer.Name;

                    cmd.Parameters.Add("P_surname", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_surname"].Value = m.Employer.Surname;

                    cmd.Parameters.Add("P_card", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_card"].Value = m.Employer.Card;

                    cmd.Parameters.Add("P_phone", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_phone"].Value = m.Employer.Phone;

                    cmd.Parameters.Add("P_email", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_email"].Value = m.Employer.Email;

                    cmd.Parameters.Add("p_employe_id", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.ID = Convert.ToInt32(cmd.Parameters["p_employe_id"].Value.ToString());
                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public ResultBase DeleteEmployee(EmployDeleteModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_DELETE_employes"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;


                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("P_EMPLOYE_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_EMPLOYE_ID"].Value = m.ID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }

        public RFMAddSegmentModel RFMGetSegmentDetails(RFMSegmentDeleteModel m)
        {
            var data = new RFMAddSegmentModel();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.RFM_GET_SEGMENT_DETAILS"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_SEGMENT_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEGMENT_ID"].Value = m.ID;

                    cmd.Parameters.Add("P_SEGMENT_DETAIL_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data = new RFMAddSegmentModel
                        {// Convert.ToInt32(rd["ID"].ToString()),
                            AgeRangeID = rd["AGE_RANGE_ID"].ToString(),
                            Coordinates = rd["COORDINATES"].ToString(),
                            Filial = rd["FILIAL"].ToString(),
                            GenderID = rd["GENDER_ID"].ToString(),
                            Name = rd["NAME"].ToString()
                        };
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        public CommunicationAddResult CommunicationAdd(CommunicationAddModel m)
        {
            CommunicationAddResult res = new CommunicationAddResult();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_COMMUN_ADD_COMMUNICATION"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;

                    cmd.Parameters.Add("P_NAME", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_NAME"].Value = m.Name;

                    cmd.Parameters.Add("P_SEGMENT_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEGMENT_ID"].Value = m.SegmentID;

                    cmd.Parameters.Add("P_SEGMENT_CNT", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEGMENT_CNT"].Value = m.SegmentCount;

                    cmd.Parameters.Add("P_SEND_DATE", OracleDbType.Date).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_SEND_DATE"].Value = m.SendDate;

                    cmd.Parameters.Add("P_MSG_TEXT", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_MSG_TEXT"].Value = m.MsgText;

                    cmd.Parameters.Add("P_TEST_GROUP_ADD", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_TEST_GROUP_ADD"].Value = m.TestGroupAdd ? 1 : 0;

                    cmd.Parameters.Add("P_BLACK_LIST_ADD", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_BLACK_LIST_ADD"].Value = m.BlackListAdd.HasValue && m.BlackListAdd.Value ? 1 : (Int32?)null;

                    cmd.Parameters.Add("P_GENDER_ID", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_GENDER_ID"].Value = m.GenderRangeID;

                    cmd.Parameters.Add("P_AGE_RANGE_ID", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_AGE_RANGE_ID"].Value = m.AgeRangeID;

                    cmd.Parameters.Add("P_DESCRIPTION", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_DESCRIPTION"].Value = m.Description;

                    cmd.Parameters.Add("P_CRM_COMMUN_LIST_EDIT_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CRM_COMMUN_LIST_EDIT_ID"].Value = m.CommunicationID;

                    cmd.Parameters.Add("P_CRM_COMMUN_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                    res.ID = cmd.Parameters["P_CRM_COMMUN_LIST_ID"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;
        }

        public List<CommunicationItem> CommunicationsGet(ModelBase m)
        {
            var data = new List<CommunicationItem>();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_COMMUN_GET_COMMUNICATIONS"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_org_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_org_id"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_ID"].Value = m.UserID;

                    cmd.Parameters.Add("P_COMMUNICATION_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.Add(new CommunicationItem
                        {
                            ID = Convert.ToInt32(rd["ID"].ToString()),
                            Name = rd["NAME"].ToString(),
                            Status = rd["STATUS"].ToString(),
                            StatusName = rd["STATUS_NAME"].ToString(),
                            SendDate = rd["SEND_DATE"] != DBNull.Value ? (DateTime?)rd["SEND_DATE"] : (DateTime?)null
                        });
                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }

        public CummunicationDetails GetCummunicationDetails(CummunicationDetailsModel m)
        {
            var data = new CummunicationDetails();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_COMMUN_GET_COMMUNICAT_DET"
            };

            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("p_org_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_org_id"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_ID"].Value = m.UserID;

                    cmd.Parameters.Add("P_CRM_COMMUN_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CRM_COMMUN_LIST_ID"].Value = m.ID;

                    cmd.Parameters.Add("P_COMMUNICATION_DET_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        data.CommunicationID = rd["ID"] != DBNull.Value ? Convert.ToInt32(rd["ID"].ToString()) : (int?)null;
                        data.Name = rd["NAME"] != DBNull.Value ? rd["NAME"].ToString() : null;

                        data.MsgText = rd["MSG_TEXT"] != DBNull.Value ? rd["MSG_TEXT"].ToString() : null;

                        data.SegmentCount = rd["SEGMENT_CNT"] != DBNull.Value ? Convert.ToInt32(rd["SEGMENT_CNT"].ToString()) : (int?)null;
                        data.SegmentID = rd["SEGMENT_ID"] != DBNull.Value ? Convert.ToInt32(rd["SEGMENT_ID"].ToString()) : (int?)null;

                        data.SendDate = rd["SEND_DATE"] != DBNull.Value ? (DateTime?)rd["SEND_DATE"] : (DateTime?)null;

                        data.TestGroupAdd = rd["TEST_GROUP_ADD"] != DBNull.Value && rd["TEST_GROUP_ADD"].ToString() == "1";
                        data.BlackListAdd = rd["BLACK_LIST_ADD"] != DBNull.Value ? rd["BLACK_LIST_ADD"].ToString() == "1" : (bool?)null;

                        data.Description = rd["Description"] != DBNull.Value ? rd["Description"].ToString() : null;
                        data.AgeRangeID = rd["age_range_id"] != DBNull.Value ? rd["age_range_id"].ToString() : null;
                        data.GenderRangeID = rd["gender_id"] != DBNull.Value ? rd["gender_id"].ToString() : null;

                    }

                    rd.Close();
                    con.Close();
                    rd.Dispose();

                    return data;
                }
                catch (OracleException oe)
                {
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
            return data;
        }


        public ResultBase CommunicationDelete(CummunicationDetailsModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_COMMUN_DELETE_COMMUNICAT"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;


                    cmd.Parameters.Add("P_CRM_COMMUN_LIST_ID_FOR_DEL", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CRM_COMMUN_LIST_ID_FOR_DEL"].Value = m.ID;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }


        public ResultBase CommunicationSendTestMsg(CommunicationSendTestMsgModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_COMMUN_SEND_TEST_MSG"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;


                    cmd.Parameters.Add("p_MSG_TEXT", OracleDbType.Varchar2, 0).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_MSG_TEXT"].Value = m.MsgText;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }

        public ResultBase CRMCampStartCampaign(CRMCampStartCampaignModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_CAMP_START_CAMPAIGN"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;


                    cmd.Parameters.Add("P_CRM_CAMP_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CRM_CAMP_LIST_ID"].Value = m.CampID;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }


        public ResultBase CRMCommunicationStart(CommunicationIDModel m)
        {
            ResultBase res = new ResultBase();

            var cmd = new OracleCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "wf.crm_pack.CRM_COMMUN_START_COMMUNICATION"
            };


            using (var con = Connect(constr))
            {
                try
                {
                    cmd.Connection = con;

                    cmd.Parameters.Add("P_ORG_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_ORG_ID"].Value = m.OrgID;

                    cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["p_user_id"].Value = m.UserID;


                    cmd.Parameters.Add("P_CRM_COMMUN_LIST_ID", OracleDbType.Int32).Direction = ParameterDirection.Input;
                    cmd.Parameters["P_CRM_COMMUN_LIST_ID"].Value = m.ID;


                    cmd.Parameters.Add("P_ERROR_ID", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_ERROR_MSG", OracleDbType.NVarchar2, 4000).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    res.Result = Convert.ToInt32(cmd.Parameters["P_ERROR_ID"].Value.ToString());
                    res.ResultMsg = cmd.Parameters["P_ERROR_MSG"].Value.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Connection.Close();
                }

            }
            return res;

        }
    }
}