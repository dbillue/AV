package org.av;

import java.sql.*;
import java.util.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Batch_Insert
{
    public int InsertData(List personList) throws SQLException
    {
        Statement stmt = null;
        Connection conn = null;
        
        String[] splitdata;
        String sqlInsert, data;
        String personId = null, lastName = null, firstName = null;
        int iRead;
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:thin:AV/Alli3@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Setup statements for batch insertion / processing.
            stmt = conn.createStatement();
            
            // Iterate thru list and add to batch.
            for(int icntr = 0; icntr < personList.size(); icntr++)
            {
                data = (String)personList.get(icntr);
                splitdata = data.split(",");

                for(iRead = 0; iRead <= splitdata.length; iRead++)
                {
                    switch(iRead)
                    {
                        case 0:
                            personId = splitdata[iRead];
                            break;
                        case 1:
                            lastName = splitdata[iRead];
                            break;
                        case 2:
                            firstName = splitdata[iRead];
                            break;
                        default:
                            break;
                    }
                }
                
             // Write data to console.
             System.out.println(personId + "," + lastName + "," + firstName);
             
             // Create SQL statement.
             sqlInsert = "INSERT INTO person (PERSONID, LAST_NAME, FIRST_NAME) VALUES "
                     + "(" + personId + ", '" + lastName + "', '" + firstName + "')"; 

             System.out.println(sqlInsert);
             
             // Add insert statement to batch.
             stmt.addBatch(sqlInsert);
            }
            
            int[] executeBatch = stmt.executeBatch();
            
            return 1;
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
            if(stmt != null)
            {
                stmt.close();
            }   
        }
    }
}