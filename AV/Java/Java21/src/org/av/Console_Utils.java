package org.av;

import java.io.*;

public class Console_Utils
{
    // Reads input from console.
    static String readEntry(String prompt)
    {
        try
        {
            StringBuffer buffer = new StringBuffer();
            System.out.println(prompt);
            System.out.flush();
            
            int c = System.in.read();
            while(c != '\n' && c != -1)
            {
              buffer.append((char)c);
              c = System.in.read();
            }
            
            return buffer.toString().trim();
        } catch (IOException e) {
            return "";
        }
    }
}