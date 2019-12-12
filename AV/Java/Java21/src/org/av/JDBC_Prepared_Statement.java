package org.av;

import java.util.Date;
import java.sql.*;
import oracle.jdbc.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Prepared_Statement
{
    public static void main (String args[]) throws SQLException
    {
        Connection conn = null;
        PreparedStatement pstmt = null;
        
        String URL, insertStmt, order_items_json = "";
        Integer order_number = 0;

        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            URL = "jdbc:oracle:thin:AV/Alli3@//192.168.0.4:1521/orclpdb";
            ods.setURL(URL);
            conn = ods.getConnection();
            
            // Create and execute preparedstatement for ORDERS table.
            order_number = 5;
            insertStmt = "INSERT INTO orders (order_number, order_date, order_status, order_total) VALUES (?, ?, ?, ?)";
            pstmt = conn.prepareStatement (insertStmt);
            pstmt.setInt(1, order_number);
            pstmt.setTimestamp(2, new Timestamp(System.currentTimeMillis()));
            pstmt.setInt(3, 1);
            pstmt.setDouble(4, 25.00);
            pstmt.execute();
            
            // Create and execute preparedstatement for ORDERITEMS table.
            order_items_json = "{name:thermal socks;name:thurmond hat}";
            insertStmt = "INSERT INTO orderitems (order_id, orderitems_json_list) VALUES (?, ?)";
            pstmt = conn.prepareStatement (insertStmt);
            pstmt.setInt(1, order_number);
            pstmt.setString(2, order_items_json);
            pstmt.execute();
            
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
}