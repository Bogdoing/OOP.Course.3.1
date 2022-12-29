namespace OOP.Course31._1;

public class NumLocomotiveDate
{
    public string NLD = "";
    public string TypeLoc = "";
    public string Pod_active = "";
    public string Num_Loc = "";
    
    public string GetType(char[] b)
    {
        string typeLoc = "";
        // 0 - паровозы; 1 - электровозы односекционные; 2 - электровозы многосекционные; 
        // 3 - электропоезда;  4 - метрополитен; 5 - тепловозы односекционные; 
        // 6 - тепловозы многосекционные; 7 - дизель-поезда и автомотрисы; 
        // 8 - специальный тяговый подвижной состав (мотовозы, автодрезины и т.д.); 9 - путевые машины.
        if (b[1] == '0') typeLoc = " Паровоз";
        else if (b[1] == '1') typeLoc = " Электровоз односекционный";
        else if (b[1] == '2') typeLoc = " Электровоз многосекционный";
        else if (b[1] == '3') typeLoc = " Электропоезд";
        else if (b[1] == '4') typeLoc = " Метрополитен";
        else if (b[1] == '5') typeLoc = " Тепловоз односекционный";
        else if (b[1] == '6') typeLoc = " Тепловозы многосекционный";
        else if (b[1] == '7') typeLoc = " Дизель-поезд и автомотрис";
        else if (b[1] == '8') typeLoc = " Специальный тяговый подвижной состав (мотовозы, автодрезины и т.д.)";
        else if (b[1] == '9') typeLoc = " Путевые машины";

        this.TypeLoc = typeLoc;
        return typeLoc;
    }

    public string GetRodActiv(char[] b)
    {
        string rodSl = "";
        if (b[2] == '0') rodSl = " Пассажирский, скоростной";
        else if (b[2] == '1') rodSl = " Грузовой, скоростной";
        else if (b[2] == '2' || b[2] == '3' || b[2] == '4') rodSl = " Грузовой";
        else if (b[2] == '5' || b[2] == '6' || b[2] == '7') rodSl = " Резерв";
        else if (b[2] == '8') rodSl = " Пассажирский, двойного питания";
        else if (b[2] == '9') rodSl = " Пассажирский, переменного тока";

        this.Pod_active = rodSl;
        return rodSl;
    }

    public string GetNum(char[] b)
    {
        string numLoc = b[3] + " " + b[4] + " " + b[5] + " " + b[6];
        this.Num_Loc = numLoc;
        return numLoc;
    }
}