package org.av;

import java.io.*;

class File_Write
{
    public static void main (String args[]) throws IOException
    {
        FileWriter fleWriter = null;
        String fleOut = "C:\\Source\\AV\\Java\\Java21\\src\\org\\av\\Out.dat";
        String outData = "";
        
        try
        {
            // Output file.
            fleWriter = new FileWriter(fleOut, true);

            // Write to file.
            outData = "123ABC";
            fleWriter.write(outData + "\n");

            // Close filewriter object / stream.
            fleWriter.close();  
        } catch (Exception e) {
            System.out.println("-------------");
            System.out.println("Error: " + e.getMessage());
        } finally {
            System.out.println("Appliction execution completed.");
        }
    }
}