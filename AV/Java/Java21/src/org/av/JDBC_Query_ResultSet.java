package org.av;

import java.sql.Connection;
import java.sql.Statement;
import java.sql.ResultSet;
import java.sql.SQLException;

import oracle.jdbc.pool.OracleDataSource;

class JDBC_Query_ResultSet
{
    public static void main (String args[]) throws SQLException
    {
        Connection conn = null;
        Statement stmt = null;
        ResultSet rset = null;
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            //ods.setURL("jdbc:oracle:oci:C##NetBeans/BrightLight@192.168.0.4:1521/orcl");
            ods.setURL("jdbc:oracle:thin:C##NetBeans/BrightLight@192.168.0.4:1521/orcl");
            conn = ods.getConnection();
            
            // Query database.
            stmt = conn.createStatement();
            rset = stmt.executeQuery("select * from departments order by department_id");
            
            // Write data to console.
            while(rset.next())
            {
                System.out.println(rset.getInt("department_id") + "," + 
                                   rset.getString("department_name") + "," +
                                   rset.getLong("manager_id") + "," +
                                   rset.getLong("location_id"));
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
            if(stmt != null)
            {
                stmt.close();
            }
            if(rset != null)
            {
                rset.close();
            }
        }
    }
}