using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymWorkout.Commands
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Workouts = new RoutedUICommand
        (
            "Workouts",
            "Workouts",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.L, ModifierKeys.Alt)
            }
        );

        public static readonly RoutedUICommand WorkoutAdd = new RoutedUICommand
        (
            "WorkoutAdd",
            "WorkoutAdd",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.A, ModifierKeys.Alt)
            }
        );
        public static readonly RoutedUICommand WorkoutEdit = new RoutedUICommand
        (
            "WorkoutEdit",
            "WorkoutEdit",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Alt)
            }
        );
        public static readonly RoutedUICommand WorkoutDelete = new RoutedUICommand
        (
            "WorkoutDelete",
            "WorkoutDelete",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.D, ModifierKeys.Alt)
            }
        );

        public static readonly RoutedUICommand WorkoutDetailEdit = new RoutedUICommand
        (
            "WorkoutDetailEdit",
            "WorkoutDetailEdit",
            typeof(CustomCommands)
        );
        public static readonly RoutedUICommand WorkoutDetailDelete = new RoutedUICommand
        (
            "WorkoutDetailDelete",
            "WorkoutDetailDelete",
            typeof(CustomCommands)
        );
    }
}
