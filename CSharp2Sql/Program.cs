
using Microsoft.Data.SqlClient;
using CSharp2Sql;
using System;

var connStr = @"server=localhost\sqlexpress;"
                + "database=EdDb;"
                + "trustServerCertificate=true;"
                + "trusted_connection=true;";

var conn = new SqlConnection(connStr);

conn.Open();

if(conn.State != System.Data.ConnectionState.Open) {
    Console.WriteLine("Connection failed to open");
    return;
}

Console.WriteLine("Connection opened successfully");

var pk = 1;
var sql = $"select * from student Where Id = {pk};";

var cmd = new SqlCommand(sql, conn);

var reader = cmd.ExecuteReader();

//var students = new List<Student>();

//while(reader.Read()) {
if(!reader.HasRows) {
    reader.Close();
    conn.Close();
    return;
}

reader.Read();

    var student = new Student();
    student.Id = Convert.ToInt32(reader["Id"]);
    student.Firstname = Convert.ToString(reader["Firstname"]);
    student.Lastname = Convert.ToString(reader["Lastname"]);
    student.StateCode = Convert.ToString(reader["StateCode"]);
    student.SAT = reader["SAT"] == System.DBNull.Value
                ? (int?)null
                : Convert.ToInt32(reader["SAT"]);
    student.GPA = Convert.ToDecimal(reader["GPA"]);
    student.MajorId = reader["MajorId"] == DBNull.Value
                ? null
                : Convert.ToInt32(reader["MajorId"]);

    //students.Add(student);
    Console.WriteLine(student);
//}

reader.Close();

conn.Close();

var x = 0;