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
            ods.setURL("jdbc:oracle:thin:AV/Alli3@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Create and execute DDL statement.
            ddlSQL = "CREATE TABLE Estate(Address varchar2(250), Name varchar2(100))";
            stmt = conn.createStatement();
            stmt.executeUpdate(ddlSQL);
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