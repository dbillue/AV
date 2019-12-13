package org.av;

import java.io.*;
import java.sql.*;
import oracle.jdbc.*;
import oracle.jdbc.OracleTypes;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Query_Cursor
{
    public static void main (String args[]) throws SQLException
    {
        Connection conn = null;
        CallableStatement cstmt = null;
        ResultSet rset = null;
        
        String procSQL, output = "";
        Integer account_Mgr_Id = 147;
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            //ods.setURL("jdbc:oracle:oci:C##NetBeans/BrightLight@192.168.0.4:1521/orcl");
            ods.setURL("jdbc:oracle:thin:OE/Spindle@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Call procedure (set parameters).
            procSQL = "{call Get_Customer_By_Account_Mgr(?, ?)}";
            cstmt = conn.prepareCall(procSQL);
            cstmt.setInt(1, account_Mgr_Id);
            cstmt.registerOutParameter(2, OracleTypes.CURSOR);
            cstmt.executeQuery();
            
            // Retrieve results and write to console.
            rset = ((OracleCallableStatement)cstmt).getCursor(2);
            while(rset.next())
            {
                // Create output data.
                output = rset.getInt("customer_id") + "," + 
                        rset.getString("cust_first_name") + "," + 
                        rset.getString("cust_last_name");
                // Write to console.
                System.out.println(output);
            }
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
        } finally {
            // Close / release database objects.
            if(conn != null)
            {
                conn.close();
            }
            if(cstmt != null)
            {
                cstmt.close();
            }
            if(rset != null)
            {
                rset.close();
            }
        }
    }
}