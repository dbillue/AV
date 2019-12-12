package org.av;

import java.sql.*;
import oracle.jdbc.pool.OracleDataSource;

public class JDBC_Procedure_Call
{
    public static void main (String args[]) throws SQLException
    {
        Connection conn = null;
        CallableStatement cstmt = null;
        
        String procSQL = "";
        Integer customer_id = 149;
        Integer order_count = 0;
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:thin:OE/Spindle@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Call procedure (set parameters).
            procSQL = "{call CUSTOMER_ORDER_COUNT(?, ?)}";
            cstmt = conn.prepareCall(procSQL);
            cstmt.setInt(1, customer_id);
            cstmt.registerOutParameter(2, Types.INTEGER);
            cstmt.executeQuery();
            
            // Retrieve results.
            order_count = cstmt.getInt(2);
            System.out.println("Customer " + customer_id + " order count: " + order_count);
            
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
        }
    }
}