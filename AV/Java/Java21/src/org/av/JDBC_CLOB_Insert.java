package org.av;

import java.io.*;
import java.sql.*;
import java.util.Scanner;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_CLOB_Insert
{
    public static void main (String args[]) throws SQLException
    {
        Connection conn = null;
        PreparedStatement pstmt = null;
        
        String flePath = "C:\\Data\\Resume_Files\\pd_cc_pkg.txt";
        String sql;
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:thin:JC/GreenMile@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            Clob clob = conn.createClob();
            String strClob = readfile(flePath);
            clob.setString(2, strClob);
            
            System.out.println("Length of Clob: " + clob.length());
            
            sql = "INSERT INTO COFFEE_DESCRIPTIONS " +
                        "VALUES(?,?)";
        
            pstmt = conn.prepareStatement(sql);
            pstmt.setString(1, "French_Roast");
            //pstmt.setString(2, strClob);
            pstmt.setClob(2, clob);
            pstmt.executeUpdate();
            
            
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage()); 
        } finally {
             // Close / release database objects.
            if(conn != null)
            {
                conn.close();
            }
            if(pstmt != null)
            {
                pstmt.close();
            }  
        }
    }
    
    public static String readfile(String flePath)
    {
        Scanner fleScanner = null;
        String fleData = null;
        
        try
        {
            // Use Scanner class.
            fleScanner = new Scanner(new File(flePath));
            
            // Write data to console.
            while(fleScanner.hasNext())
            {
               fleData += fleScanner.nextLine() + System.lineSeparator();
            }
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
        }
        
        return fleData;
    }
}