package org.av;

import java.sql.*;
import oracle.jdbc.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Prepared_Statement_DDL
{
    public static void main (String args[]) throws SQLException
    {
        Connection conn = null;
        Statement stmt = null;
        
        String ddlSQL = "";
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:thin:JC/GreenMile@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Create and execute DDL statement.
            ddlSQL = "CREATE TYPE MANAGER AS OBJECT " +
                    "(MGR_ID INTEGER, LAST_NAME " +
                    "VARCHAR(40), " +
                    "FIRST_NAME VARCHAR(40), " +
                    "PHONE PHONE_NO)";
            stmt = conn.createStatement();
            stmt.executeUpdate(ddlSQL);
            System.out.println("Type created.");
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
        } finally {
             // Close / release database objects.
            if(conn != null)
            {
                conn.close();
            }
            if(stmt != null)
            {
                stmt.close();
            }           
        }
    }
}