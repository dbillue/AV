//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

class MarsRobot
{
    String _status;
    int _speed;
    float _temperature;
    
    MarsRobot(String status, int speed, float temperature)
    {
       _status = status;
       _speed = speed;
       _temperature = temperature;
    }
    
    void checkTemperature()
    {
        if(_temperature > -80)
        {
            _status = "Returning home...";
        }
    }
    
    void showAttributes()
    {
        System.out.println("Status: " + _status);
        System.out.println("Speed: " + _speed);
        System.out.println("Temperature: " + _temperature);
    }
}