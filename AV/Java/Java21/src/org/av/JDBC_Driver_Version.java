import java.sql.*;
import oracle.jdbc.*;
import oracle.jdbc.pool.OracleDataSource;

class JDBC_Driver_Version
{
  public static void main (String args[]) throws SQLException
  {
    try
    {
        OracleDataSource ods = new OracleDataSource();
        ods.setURL("jdbc:oracle:thin:AV/Alli3@192.168.0.4:1521/orclpdb");
        Connection conn = ods.getConnection();

        // Create Oracle DatabaseMetaData object
        DatabaseMetaData meta = conn.getMetaData();

        // gets driver info:
        System.out.println("JDBC driver version is " + meta.getDriverVersion());
    } catch (Exception e) {
        System.err.println(e.getMessage());
    }
  }
}