package org.av;

import java.sql.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Query_UDT
{
    public static void main(String args[]) throws SQLException
    {
        Connection conn = null;
        Statement stmt = null;
        ResultSet rset = null;
        
        try
        {
            // Connect to database.
            OracleDataSource ods = new OracleDataSource();
            ods.setURL("jdbc:oracle:thin:JC/GreenMile@192.168.0.4:1521/orclpdb");
            conn = ods.getConnection();
            
            // Query database.
            stmt = conn.createStatement();
            rset = stmt.executeQuery("select * from stores order by store_no");
            
            // Write data to console.
            while(rset.next())
            {
               int storeNo = rset.getInt("Store_No");
               Struct location = (Struct)rset.getObject("Location");
               Object[] locAttributes = location.getAttributes();
               Array arrCoffeeTypes = rset.getArray("Cof_Types");
               String[] cofTypes = (String[])arrCoffeeTypes.getArray();
               Struct manager = (Struct)rset.getObject("Mgr");
               Object[] mgrAttributes = manager.getAttributes();
               
               // Store number.
               System.out.println("Store No: " + 
                                    System.lineSeparator() + 
                                    "----------" + 
                                    System.lineSeparator() +
                                    storeNo +
                                    System.lineSeparator());
               
               // Store location.
               System.out.println("Store Location: " + 
                                    System.lineSeparator() + 
                                    "----------");
               
               for(int icnt = 0; icnt < locAttributes.length; icnt++)
               {
                  System.out.println(locAttributes[icnt]); 
               }
               
               System.out.println(System.lineSeparator());
               
               // Coffee types.
               System.out.println("Coffee Types: " + 
                                    System.lineSeparator() + 
                                    "----------");

               for(int icnt = 0; icnt < cofTypes.length; icnt++)
               {
                   System.out.println(cofTypes[icnt]);
               }
               
               // Managers.
               System.out.println(System.lineSeparator());
               
               System.out.println("Manager: " + 
                                    System.lineSeparator() + 
                                    "----------");
               
               for(int icnt = 0; icnt < mgrAttributes.length; icnt++)
               {
                   // Check for Phone_No struct / UDT.
                   switch(icnt)
                   {
                       case 3:
                            Struct phone = (Struct)mgrAttributes[icnt];
                            Object[] mgrPhone = phone.getAttributes();
                            System.out.println(mgrPhone[0]);
                           break;
                       default:
                           System.out.println(mgrAttributes[icnt]);
                           break;
                   }
               }
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