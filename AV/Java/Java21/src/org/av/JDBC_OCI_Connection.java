package org.av;

import java.sql.*;
import oracle.jdbc.*;
import oracle.jdbc.pool.OracleDataSource;
import java.io.*;

class JDBC_OCI_Connection
{
    public static void main (String args[]) throws SQLException, IOException
    {
        Console_Utils cUtils = new Console_Utils();
        
        Connection conn = null;
        Statement stmt = null;
        ResultSet rset = null;
        
        String database, user, password;
        
        try
        {
            // Obtain user inputs for database connection.
            database = cUtils.readEntry("Enter database:");
            user = cUtils.readEntry("Enter user:");
            password = cUtils.readEntry("Enter password:");

            System.out.println("Database: " + database + "\n" +
                                "User: " + user + "\n" +
                                "Password: " + password);

            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:oci:@" + database);
            ods.setUser(user);
            ods.setPassword(password);
            conn = ods.getConnection();
            
            // Query database.
            stmt = conn.createStatement();
            rset = stmt.executeQuery("SELECT sysdate FROM DUAL");
            
            // Write data to console.
            while(rset.next())
            {
                System.out.println("dual.sysdate: " + rset.getString(1));
            }

            System.out.println("The JDBC installation is correct.");
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
        } finally {
            // Close / release database objects.
            if(conn != null)
            {
                conn.close();
            }
            if(rset != null)
            {
                rset.close();
            }
            if(stmt != null)
            {
                stmt.close();
            }
        }
    }
}