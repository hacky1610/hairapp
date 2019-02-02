using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Models;

namespace HairAppBl.Controller
{
    public class WashingDayEditorController
    {
        readonly WashingDayDefinition mWashingDay;
        public WashingDayEditorController(WashingDayDefinition wd)
        {
            if(wd == null)
                throw new ArgumentNullException("wd");

            this.mWashingDay = wd;
            FillUnusedDefitions();
        }

        public void FillUnusedDefitions()
        {
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Prepoo", "Prepoo","", "Please do your Prepoo"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Hot Oil Treatment", "HotOilTreatment", "", "Please do your Hot Oil Treatment"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Shampoo", "Shampoo", "", "Please wash your hair"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Carifying SHampoo", "CarifyingShampoo", "", "Please wash your hair"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Conditioner", "Conditioner", "", "Please use your Conditioner"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Deep Conditioner", "DeepConditioner", "", "Please use your Deep Conditioner"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Protein Treatment", "ProteinTreatment", "", "Please make Protein Treatment"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Moisturising Mask", "MoisturisingMask", "", "Please make Moisturising Mask"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Leave in Conditioner", "LeaveInConditioner", "", "Please make Leave in Conditioner"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Clay", "Clay", "", "Please use Clay"));
            this.mWashingDay.UnusedDefitions.Add(RoutineDefinition.Create("Rinses", "Rinses", "", "Please use Rinses"));
        }

        public void AddRoutine(RoutineDefinition routine)
        {
            if(routine == null)
                throw new ArgumentNullException("routine");
            if(routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            this.mWashingDay.UnusedDefitions.Remove(routine);

            this.mWashingDay.Routines.Add(routine);
        }

        public void RemoveRoutine(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("Routine is null");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            this.mWashingDay.Routines.Remove(routine);

            this.mWashingDay.UnusedDefitions.Add(routine);
        }

        public void MoveUp(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("Routine is null");
            //if(this.mWashingDay.Routines.ContainsKey(routine.ID))
            //    throw new ArgumentException($"Routine {routine.Name} was already added");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            var currentIndex = this.mWashingDay.Routines.IndexOf(routine);
            if (currentIndex == 0)
                return;

            this.mWashingDay.Routines.Remove(routine);
            this.mWashingDay.Routines.Insert(currentIndex - 1, routine);
        }

        public void MoveDown(RoutineDefinition routine)
        {
            if (routine == null)
                throw new ArgumentNullException("Routine is null");
            //if(this.mWashingDay.Routines.ContainsKey(routine.ID))
            //    throw new ArgumentException($"Routine {routine.Name} was already added");
            if (routine.ID == string.Empty)
                throw new ArgumentException($"Routine ID is empty");

            var currentIndex = this.mWashingDay.Routines.IndexOf(routine);
            if (currentIndex == this.mWashingDay.Routines.Count -1)
                return;

            this.mWashingDay.Routines.Remove(routine);
            this.mWashingDay.Routines.Insert(currentIndex + 1, routine);
        }

        public List<RoutineDefinition> GetRoutineDefinitions()
        {
            return this.mWashingDay.Routines;
        }

        public List<RoutineDefinition> GetUnusedRoutines()
        {
            return this.mWashingDay.UnusedDefitions;
        }

    }
}
