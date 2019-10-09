//*********************
// Author:          Rogers Cadenhead / Duane Billue
// Date:            2019-10-09
// Description:     Chapter 1 - Getting Started
//*********************

class MarsApplication
{
    public static void main(String[] arguments)
    {
        MarsRobot spirit = new MarsRobot("Exploring...", 2, -60);
        
        // Show atributes of robot.
        spirit.showAttributes();
        
        System.out.println("Increasing speed to 3 MPH");
        spirit._speed = 5;
        spirit._temperature = -20;
        
        // Show atributes of robot.
        // spirit.showAttributes(); 
        
        // Check stats.
        spirit.checkTemperature();
        
        // Show atributes of robot.
        spirit.showAttributes(); 
    }
}