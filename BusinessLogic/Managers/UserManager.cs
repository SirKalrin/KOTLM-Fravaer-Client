using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTimeExtensions;
using ServiceGateways.Entities;
using ServiceGateways.Facade;
using ServiceGateways.Interfaces;

namespace BusinessLogic.Managers
{
    public class UserManager
    {
        private IServiceGateway<Absence, int> _absenceServiceGateway = new ServiceGatewayFacade().GetAbsenceServiceGateway();

        private readonly ArrayList _typeList = new ArrayList()
            {
                "S - Syg",
                "HS - ½sygedag",
                "F - Ferie",
                "HF - ½feriedag",
                "FF - Feriefridag",
                "HFF - ½Feriefridag",
                "K - Kursus",
                "B - Barsel",
                "BS - Barn 1. sygedag",
                "AF - Andet fravær",
                "A - Afspadsering",
                "HA - ½afspadsering",
                "SN - Seniordag",
                "Slet"
            };

        private readonly ArrayList _daysList = new ArrayList() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };


        public void AddAbsenceToUser(User user, DateTime? absenceDate, string chosenAbsence)
        {
                if (!chosenAbsence.Equals("delete"))
                {
                    var newAbsence = new Absence()
                    {
                        Date = absenceDate.Value,
                        Status = GetStatus(chosenAbsence, _typeList),
                        User = user
                    };
                    _absenceServiceGateway.Create(newAbsence);
                    
                }
        }

        public void DeleteAbsenceFromUser(int id)
        {
            _absenceServiceGateway.Delete(id);
        }


        private Statuses GetStatus(string statusText, ArrayList statusList)
        {
            switch (statusList.IndexOf(statusText))
            {
                case 0:
                    return Statuses.S;
                case 1:
                    return Statuses.HS;
                case 2:
                    return Statuses.F;
                case 3:
                    return Statuses.HF;
                case 4:
                    return Statuses.FF;
                case 5:
                    return Statuses.HFF;
                case 6:
                    return Statuses.K;
                case 7:
                    return Statuses.B;
                case 8:
                    return Statuses.BS;
                case 9:
                    return Statuses.AF;
                case 10:
                    return Statuses.A;
                case 11:
                    return Statuses.HA;
                case 12:
                    return Statuses.SN;
            }
            return Statuses.K;
        }


        public ArrayList GetAbsenceTypes()
        {
            return _typeList;
        }

        public int GetInitIndex(DateTime chosenMonth)
        {
            return _daysList.IndexOf(chosenMonth.FirstDayOfTheMonth().DayOfWeek.ToString());
        }
    }
}
