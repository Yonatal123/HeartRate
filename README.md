# HeartRate
How to use?
Two methods:
1. Heart Rate data simulated from the same program of the GUI
    a.In UIConfig.json set the value of "UseSerialPortSimulator" to false.
    b.Run HeartRate project
2. Heart Rate device is simulated as well as two virtual Serial Ports (PC and device)
    a. Download com0com (Null modem eulator) from here: https://sourceforge.net/projects/com0com/files/com0com/
    b. Install the emulator and open the setup window.
    c. Add ne virtual port pair and name the ports COM20 and COM21.
    d. In UIConfig.json set the value of "UseSerialPortSimulator" to true.
    e. Make sure that in UIConfig.json of HeartRate project and in CommConfig.json of SerialPortDeviceSimulator SerialPortNames 
       are set to COM20 and COM21
    f. Run both HeratRate and SerialPortDeviceSimulator projects
    
