package org.av;

import java.sql.*;

class JDBC_Utils_Warning
{
    public static void printWarnings(SQLWarning warning) throws SQLException 
    {
        if (warning != null) 
        {
            System.out.println("\n---Warning---\n");

            while (warning != null) 
            {
                System.out.println("Message: " + warning.getMessage());
                System.out.println("SQLState: " + warning.getSQLState());
                System.out.print("Vendor error code: ");
                System.out.println(warning.getErrorCode());
                System.out.println("");
                warning = warning.getNextWarning();
            }
        }
    }
}