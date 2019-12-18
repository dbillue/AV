package org.av;

import java.io.*;
import java.util.*;

class File_Read
{
    public static void main (String args[]) throws IOException
    {
        String[] strData = null;
        FileReader fleRead = null;
        Scanner fleScanner = null;
        List<String> personList = new ArrayList();
        JDBC_File_Insert oDataInsert = new JDBC_File_Insert();
        JDBC_Batch_Insert oBatchInsert = new JDBC_Batch_Insert();
        
        String flePath = "C:\\Source\\AV\\Java\\Java21\\src\\org\\av\\out.csv";
        String personId = null, lastName = null, firstName = null, data = null;
        int iRead, insertSuccess = 0;
        
        try
        {
           /*
           // Use FileReader class.
           fleRead = new FileReader(flePath);
            
           // Write each char to console
           while((iRead = fleRead.read()) != -1)
           {               
               System.out.print((char)iRead);
           }
            
            fleRead.close();
           */
            
           // Use Scanner class.
           fleScanner = new Scanner(new File(flePath));
           
           // Write data to console.
           while(fleScanner.hasNext())
           {
             data = fleScanner.nextLine();
             strData = data.split(",");
             
             /*
             for(iRead = 0; iRead <= strData.length; iRead++)
             {
                 switch(iRead)
                 {
                     case 0:
                         personId = strData[iRead];
                         break;
                     case 1:
                         lastName = strData[iRead];
                         break;
                     case 2:
                         firstName = strData[iRead];
                         break;
                     default:
                         break;
                 }
             }
             */
             
             // Add data to list.
             personList.add(data);
             
             // Write data to console.
             //System.out.println(personId + "," + lastName + "," + firstName);

             // Write data to database.  Call JDBC_File_Insert.java.
             // insertSuccess = oDataInsert.InsertData(Integer.parseInt(personId), lastName, firstName);
           }
           
           // Write to database using batch class. Call JDBC_Batch_Insert.java.
           oBatchInsert.InsertData(personList);
           
           // Close Scanner class.
           fleScanner.close(); 
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
        } finally {
            System.out.println("Appliction execution completed.");    
        }
    }
}

