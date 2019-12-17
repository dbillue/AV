package org.av;

import java.sql.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_File_Insert
{
    Connection conn = null;
    CallableStatement cstmt = null;
    
    public int InsertData(int personId, String lastName, String firstName) throws SQLException
    {
        String procSQL;
        
        try
        {            
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:thin:AV/Alli3@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Call procedure (set parameters) and insert data.
            procSQL = "{call Add_Person(?, ?, ?)}";
            cstmt = conn.prepareCall(procSQL);
            cstmt.setInt(1, personId);
            cstmt.setString(2, lastName);
            cstmt.setString(3, firstName);
            cstmt.executeQuery();
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
            return 0;
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
        
        return 1;
    }
}