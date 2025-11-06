using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public enum DeviceType
    {
        Camera,
        MotionSensor,
        Thermostat,
        DoorLock,
        CheckInSystem,
        MetalDetector,
        AlarmSystem,
        FireSuppressionSystem,
        SurveillanceDrone
    }
    public partial class Device:ObservableObject
    {
        [ObservableProperty]
        [property:PrimaryKey]
        [property:AutoIncrement]
        int id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        DeviceType type;
    }
}
