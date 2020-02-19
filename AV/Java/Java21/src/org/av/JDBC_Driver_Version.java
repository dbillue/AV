package org.av;

import java.sql.*;
import oracle.jdbc.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Driver_Version
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
        //ods.setURL("jdbc:oracle:thin:AV/Alli3@127.0.0.1:1521/orclpdb");
        ods.setURL("jdbc:oracle:thin:C##NetBeans/BrightLight@192.168.0.4:1521/orcl");
        conn = ods.getConnection();

        // Create Oracle DatabaseMetaData object
        DatabaseMetaData meta = conn.getMetaData();

        // gets driver info:
        System.out.println("JDBC driver version is " + meta.getDriverVersion());
        System.out.println("Connected.");

        stmt = conn.createStatement();
        rset = stmt.executeQuery("SELECT sysdate FROM DUAL");

        // Write data to console.
        while(rset.next())
        {
            System.out.println("dual.sysdate: " + rset.getString(1));
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