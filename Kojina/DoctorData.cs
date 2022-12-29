namespace WinFormsApp2;

public class DoctorData
{
    public string DoctorID = ""; // clickCellTableDoctorID
    public DateTime Date;  // dt
    public string StartTime = "";  //  dccstart
    public string EndTime = "";  //dccend
    public char Status = ' ';  // status
    public char UserID = ' ';  //// i 
    public int Price; // prise
    
    // command.Parameters.AddWithValue("@DoctorID", clickCellTableDoctorID);
    // command.Parameters.AddWithValue("@Date", dt);
    // command.Parameters.AddWithValue("@StartTime", dccstart);
    // command.Parameters.AddWithValue("@EndTime", dccend);
    // command.Parameters.AddWithValue("@Status", status);
    // command.Parameters.AddWithValue("@UserID", i);
    // command.Parameters.AddWithValue("@Price", prise);
}